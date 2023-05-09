using System.Linq.Expressions;

namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IReadRepositoryBase<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        IQueryable<TEntity> GetAll(bool asNoTracking = true);
        IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);

        TEntity GetById<TId>(TId id) where TId : notnull;
        Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        TEntity GetBySpec(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> GetListAsync(CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
