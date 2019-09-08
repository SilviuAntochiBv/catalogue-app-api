using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface IAddable<in TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        /// <summary>
        /// Adds the entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the entity if succeed, null otherwise</returns>
        Task<TResult> Add(TParameter entity);
    }
}
