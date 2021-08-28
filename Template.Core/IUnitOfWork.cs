using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Template.Core.Data
{
    public interface IUnitOfWork
    {
        Task<IEnumerable<ValidationFailure>> Commit();
    }
}