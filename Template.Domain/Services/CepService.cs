using Template.Application.Queries;
using Template.Core.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Template.Domain.Services
{
    public interface ICepService
    {
        RetornoCep BuscarCep(string cep);
    }

    public class CepService : ICepService, IDomainService
    {
        public RetornoCep BuscarCep(string cep)
        {
            try
            {
                var client = new RestClient($"http://viacep.com.br/ws/{cep}/json/") { Timeout = 3000 };
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);

                return JObject.Parse(response.Content).ToObject<RetornoCep>();
            }
            catch
            {
                return new RetornoCep();
            }
        }
    }
}
