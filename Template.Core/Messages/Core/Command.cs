using System;
using MediatR;

namespace Template.Core.Messages
{
    /// <summary>
    /// Classe base do comando do CQRS
    /// </summary>
    public abstract class Command : Message, IRequest<CommonCommandResult>
    {
        protected Command()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Type = MessageType.Command;
        }
    }
}