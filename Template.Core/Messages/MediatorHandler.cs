using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;

namespace Template.Core.Messages
{

    /// <summary>
    /// Contrato do Handler do mediator
    /// </summary>
    public interface IMediatorHandler
    {
        Task<CommonCommandResult> EnviarComando<T>(T comando)
            where T : Command;

        Task<T> EnviarQuery<T>(Query<T> query);

        Task PublicarEvento<T>(T evento) where T : INotification;
    }

    /// <summary>
    /// Wrapper do mediator para facilitar o envio de Commands/Queries
    /// </summary>
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommonCommandResult> EnviarComando<T>(T comando)
            where T : Command
        {
            return await _mediator.Send(comando);
        }

        public async Task<T> EnviarQuery<T>(Query<T> query)
        {
            return await _mediator.Send(query);
        }

        public async Task PublicarEvento<T>(T evento) where T : INotification
        {
            await _mediator.Publish(evento);
        }
    }
}