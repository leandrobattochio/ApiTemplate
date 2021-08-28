using FluentValidation;
using FluentValidation.Results;
using Template.Core.Messages;

namespace Template.Application.Queries
{
    public class ConsultarCepQuery : Query<QueryResponseMessage<RetornoCep>>
    {
        public string Cep { get; private set; }

        public ConsultarCepQuery(string cep)
        {
            Cep = cep;
        }
    }

    public class ConsultarQueryValidation : AbstractValidator<ConsultarCepQuery>
    {
        public ConsultarQueryValidation()
        {
            RuleFor(c => c.Cep)
                .Custom((cep, validationContext) => 
                {
                    if (string.IsNullOrEmpty(cep))
                    {
                        validationContext.AddFailure(new ValidationFailure("Cep", "Cep em branco"));
                        return;
                    }

                    if (cep.Replace("-", "").Replace(".", "").Replace(" ", "").Length != 8)
                        validationContext.AddFailure(new ValidationFailure("Cep", "Cep invalido"));
                });
        }
    }

    public class RetornoCep
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string IBGE { get; set; }
        public string GIA { get; set; }
        public string DDD { get; set; }
        public string SIAFI { get; set; }
    }
}
