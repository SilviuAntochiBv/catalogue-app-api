using System;
using System.Linq;
using System.Linq.Expressions;
using API.Domain.Entities.Interfaces;
using API.Domain.Interfaces;

namespace API.Data.Interfaces
{
    public interface IRepository<T> :
        IAddable<T, T>,
        IInterrogable<T>,
        IUpdatable<T, T>,
        IDeletable<T>
        where T : class, IBaseEntity
    {
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> Filter(Expression<Func<T, bool>> filter);

        IQueryable<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
    }
}
