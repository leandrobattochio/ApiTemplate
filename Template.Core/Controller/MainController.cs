using FluentValidation.Results;
using Template.Core.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Template.Core
{

    /// <summary>
    /// Clase base para os Controladores, com métodos básicos para adicionar erros
    /// e padronizar a resposta em caso de sucesso ou falhas.
    /// </summary>
    public abstract class MainController : ControllerBase
    {
        protected readonly IMediatorHandler _mediator;
        protected readonly IMapper _mapper;

        protected MainController(IMediatorHandler mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected ICollection<string> Erros = new List<string>();

        /// <summary>
        /// Retorna o resultado da uma query ou retorna o erro.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected ActionResult RetornoQueryVerificaErro<T>(QueryResponseMessage<T> response)
            where T: class
        {
            // Verifica o resultado para dar o retorno adequado
            if (response.GetValidationResult().IsValid)
            {
                return RetornarOk(response.Data);
            }
            else
            {
                return RetornarErro(response.GetValidationResult());
            }
        }

        protected ActionResult RetornarErroDeValidacao(ValidationResult validationResult)
        {
            return RetornarErro(validationResult);
        }

        private ActionResult RetornarOk(object data = null)
        {
            return Ok(data);
        }

        private ActionResult RetornarErro(ValidationResult validationResult)
        {
            var erros = Erros.ToList();
            erros.AddRange(validationResult.Errors.Select(c => c.ErrorMessage));

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", erros.ToArray() }
            }));
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AdicionarErroProcessamento(mensagem);
            }

            return true;
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }

    public class ResponseResult
    {
        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}