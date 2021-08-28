using Template.Application.Queries;
using Template.Core;
using Template.Core.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.API.DTOs;
using AutoMapper;

namespace Template.API.Controllers
{
    [Route("api/v1/cep")]
    public class CepController : MainController
    {

        public CepController(IMediatorHandler _mediator, IMapper mapper) : base(_mediator, mapper)
        {

        }


        [HttpGet]
        public async Task<IActionResult> ConsultarCEP(ConsultarCepDTO dto)
        {
            var query = _mapper.Map<ConsultarCepQuery>(dto);
            var retorno = await _mediator.EnviarQuery(query);
            return RetornoQueryVerificaErro(retorno);
        }
    }
}
