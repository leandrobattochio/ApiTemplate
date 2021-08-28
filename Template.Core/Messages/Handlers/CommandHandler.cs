using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Template.Core.Data;

namespace Template.Core.Messages
{

    /// <summary>
    /// Classe base do CommandHandler que contém métodos para adicionar erros
    /// e persistencia de dados no contexto do banco de dados.
    /// </summary>
    public abstract class CommandHandler : MessageHandler
    {
        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            var errors = await uow.Commit();
            if (errors.Count() > 0)
                foreach (var error in errors)
                    AdicionarErro(error);

            return ValidationResult;
        }
    }
}