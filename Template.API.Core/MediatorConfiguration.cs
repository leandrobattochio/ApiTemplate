using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Template.Core.Messages;

namespace Template.API.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class MediatorConfiguration
    {
        public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services, Assembly asm)
        {
            services.AddMediatR(asm);

            // Ordem 1 - Primeiro valida
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Query<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var serializedRequest = JsonConvert.SerializeObject(request);

            _logger.LogInformation($"Processando request ([{request.Id}] - {request.GetType().Name}). Dados entrada: {serializedRequest}");

            var response = await next();

            UpdateFinishingTime(request);

            var baseResponse = response as BaseResponseMessage;

            if (baseResponse.GetValidationResult().IsValid == false)
            {
                var serializedResponse = JsonConvert.SerializeObject(baseResponse.GetValidationResult().Errors.Select(c => c.ErrorMessage).ToList());
                _logger.LogError($"Processado request com erros ([{request.Id}] - {request.FinalizedAt}) - Dados saida: {serializedResponse}");
                request.SetStatus(MessageStatus.Executado_Com_Erros);
            }
            else
            {
                var serializedResponse = JsonConvert.SerializeObject(response);
                _logger.LogInformation($"Processado request ([{request.Id}] - {request.FinalizedAt}) - Dados saida: {serializedResponse}");
                request.SetStatus(MessageStatus.Executado);
            }

            return response;
        }


        private void UpdateFinishingTime(TRequest request)
        {
            if (request is Query<TResponse>)
            {
                request.UpdateCompletionTime(DateTime.Now);
            }
            // ToDo: fazer para os comandos
        }
    }

    /// <summary>
    /// Pipeline de validação do MediatR
    /// Valida automaticamente as queries e commands.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();


                if (failures.Count != 0)
                {
                    var responseType = typeof(TResponse);

                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(QueryResponseMessage<>).MakeGenericType(resultType);

                    var invalidResponse =
                    Activator.CreateInstance(invalidResponseType,
                        new object[] { new ValidationResult(failures), null }
                    );

                    return (TResponse)invalidResponse;

                }

            }
            return await next();
        }
    }
}
