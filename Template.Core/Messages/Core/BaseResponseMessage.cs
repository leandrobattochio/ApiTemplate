using FluentValidation.Results;

namespace Template.Core.Messages
{
    public abstract class BaseResponseMessage
    {
        protected ValidationResult ValidationResult { get; set; }

        public ValidationResult GetValidationResult()
        {
            return ValidationResult;
        }

        public void AdicionarErros(ValidationFailure[] failure)
        {
            ValidationResult.Errors.AddRange(failure);
        }
    }
}