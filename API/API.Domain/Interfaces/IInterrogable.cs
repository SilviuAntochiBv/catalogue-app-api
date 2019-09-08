using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface IInterrogable<TResult>
        where TResult : class
    {
        /// <summary>
        /// Gets the entity by id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        Task<TResult> GetById(object id);

        /// <summary>
        /// Get all the database records for a specific entity
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TResult>> GetAll();
    }
}
