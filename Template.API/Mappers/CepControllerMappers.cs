using AutoMapper;
using Template.API.DTOs;
using Template.Application.Queries;

namespace Template.API.Mappers
{
    public class CepControllerMappers : Profile
    {
        public CepControllerMappers()
        {
            // Mapeia o DTO de consulta Cep para a query.
            CreateMap<ConsultarCepDTO, ConsultarCepQuery>();
        }
    }
}
