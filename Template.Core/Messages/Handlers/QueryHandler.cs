using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class QueryHandler : MessageHandler
    {

        /// <summary>
        /// Retorna a query com a validação de erros e a resposta, se houver.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected QueryResponseMessage<T> ValidateAndCreateResult<T> (T response = null)
            where T: class
        {
            if (ValidationResult.IsValid)
                return new QueryResponseMessage<T>(ValidationResult, response);
            else
                return new QueryResponseMessage<T>(ValidationResult);
        }
    }
}
