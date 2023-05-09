using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Models;
using System.Linq.Expressions;

namespace OnlineStore.Data.Extensions
{
    /// <summary>
    /// Some useful extension methods for <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return query.Skip(skipCount).Take(pageSize);
        }

        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="pageNumber">The pageNumber.</param>
        /// <param name="pageSize">The pageSize.</param>
        public static IQueryable<T> ApplyPaging<T>(
            this IQueryable<T> query, int pageNumber, int pageSize)
            where T : class
        {
            var skipCount = (pageNumber - 1) * pageSize;

            return query.PageBy(skipCount, pageSize);
        }

        /// <summary>
        /// Creates a <see cref="PagedList{T}" /> from an <see cref="IQueryable{T}"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> query, int pageNumber, int pageSize)
            where T : class
        {
            if (query == null)
            {
                return new PagedList<T>(new List<T>(), 0, pageNumber, pageSize);    
            }

            var count = query.Count();
            var items = await query
                .ApplyPaging(pageNumber, pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
