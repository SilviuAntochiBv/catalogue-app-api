using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface IDeletable<in TParameter>
        where TParameter : class
    {
        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        Task<bool> Delete(TParameter entity);

        /// <summary>
        /// Deletes the entity by the id that is provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);
    }
}
