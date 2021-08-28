using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Template.Core.Data;
using Template.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Core;
using System.Linq;
using Template.Core.Messages;

namespace Template.Infra.Data
{
    /// <summary>
    /// Contexto do banco de dados, juntamente com Identity Core
    /// </summary>
    public partial class LivrariaDbContext : IdentityDbContext<IdentityUser>, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;

        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options, IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Após fazer qualquer alteração no contexto, precisa chamar esse método pra "Comittar" o comando SQL feito no banco.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ValidationFailure>> Commit()
        {
            var errors = new List<ValidationFailure>();
            var sucesso = false;

            try
            {
                sucesso = await base.SaveChangesAsync() > 0;

                if(sucesso)
                {
                    await _mediator.PublicarEventos(this);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new List<ValidationFailure>();
        }
    }


    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<NotificationEntity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}