using System.Collections.Generic;
using FluentValidation.Results;

namespace Template.Core.Messages
{
    public abstract class MessageHandler
    {
        protected ValidationResult ValidationResult;

        protected MessageHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected void AdicionarErro(ValidationFailure validationFailure)
        {
            ValidationResult.Errors.Add(validationFailure);
        }

        protected void AdicionarErros(IEnumerable<ValidationFailure> validationFailures)
        {
            ValidationResult.Errors.AddRange(validationFailures);
        }
    }
}