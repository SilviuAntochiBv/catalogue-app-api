using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using API.Domain.Entities.Interfaces;

namespace API.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
            where T : class, IBaseEntity
        {
            return orderBy != null ? orderBy(query) : query;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter)
            where T : class, IBaseEntity
        {
            return filter != null ? query.Where(filter) : query;
        }

        public static IQueryable<T> IncludeProperties<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
            where T : class, IBaseEntity
        {
            return includeProperties != null ? includeProperties.Aggregate(query, (current, property) => current.Include(property)) : query;
        }

    }
}
