using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Auditing;
using OnlineStore.Data.Contracts.Interfaces;
using System.Linq.Expressions;

namespace OnlineStore.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>, IReadRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
                return _dbSet.AsNoTracking();
           
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            if (asNoTracking)
                return _dbSet
                    .Where(predicate)
                    .AsNoTracking();

            return _dbSet
                .Where(predicate)
                .AsQueryable();
        }

        public virtual TEntity GetById<TId>(TId id) where TId : notnull
        {
            return _dbSet.Find(new object[] { id });
        }   
        public virtual async Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual TEntity GetBySpec(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        public virtual async Task<TEntity> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<ICollection<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .CountAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(cancellationToken);
        }



        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        public virtual void AddRange(ICollection<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }
        public virtual async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }


        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public virtual void UpdateRange(ICollection<TEntity> entities)
        {
            _context.UpdateRange(entities);
        }


        public virtual void Delete(TEntity entity)
        {
            if (typeof(IHasDeletedDate).IsAssignableFrom(typeof(TEntity)))
            {
                (entity as IHasDeletedDate).DeletedDate = DateTimeOffset.UtcNow;
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }
        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
