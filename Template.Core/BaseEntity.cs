using System;

namespace Template.Core
{
    /// <summary>
    /// Classe abstrata para representar uma entidade.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.Empty;
        }
    }
}