using Template.Application.Queries;
using Template.Core.Messages;
using Template.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Domain.Queries.Cep
{
    public class CepQueryHandler : QueryHandler,
        IRequestHandler<ConsultarCepQuery, QueryResponseMessage<RetornoCep>>
    {
        private readonly ICepService _cepService;

        public CepQueryHandler(ICepService cepService)
        {
            _cepService = cepService;
        }

        /// <summary>
        /// Comando ja vem validado.
        /// Não precisa se preocupar.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<QueryResponseMessage<RetornoCep>> Handle(ConsultarCepQuery request, CancellationToken cancellationToken)
        {
            await Task.Yield();

            // Faz a consulta.
            var cep = request.Cep.Replace(".", "").Replace("-", "").Replace(" ", "");
            var result = _cepService.BuscarCep(cep);

            return ValidateAndCreateResult(result);
        }
    }
}
