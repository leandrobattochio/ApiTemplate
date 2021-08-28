using MediatR;
using System.Collections.Generic;

namespace Template.Core
{
    /// <summary>
    /// Classe a ser herdada por uma entidade que dispara notificações quando é salva
    /// no banco de dados.
    /// </summary>
    public abstract class NotificationEntity : BaseEntity, INotificationEntity
    {
        private List<INotification> _notificacoes { get; set; }
        public IReadOnlyCollection<INotification> Notificacoes => _notificacoes?.AsReadOnly();

        protected NotificationEntity() : base()
        {

        }

        public void AdicionarNotificacao(INotification notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }
    }

    public interface INotificationEntity
    {

    }
}