using FluentValidation.Results;

namespace Template.Core.Messages
{
    /// <summary>
    /// Resposta de uma query do CQRS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResponseMessage<T> : BaseResponseMessage
        where T : class
    {
        public T Data { get; private set; }

        public QueryResponseMessage()
        {
            ValidationResult = new ValidationResult();
        }

        public QueryResponseMessage(ValidationResult validationResult, T data = null)
        {
            ValidationResult = validationResult;
            Data = data;
        }
    }
}