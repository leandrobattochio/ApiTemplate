using System;
using FluentValidation.Results;

namespace Template.Core.Messages
{
    /// <summary>
    /// Classe de retorno padrão dos Commands do CQRS
    /// </summary>
    public class CommonCommandResult : BaseResponseMessage
    {
        public Guid EntityId { get; set; }

        public CommonCommandResult(ValidationResult validationResult, Guid entityId)
        {
            ValidationResult = validationResult;
            EntityId = entityId;
        }

        public CommonCommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}