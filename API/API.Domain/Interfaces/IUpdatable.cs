using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface IUpdatable<in TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the entity if succeed, null otherwise</returns>
        Task<TResult> Update(TParameter entity);
    }
}
