namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRange(ICollection<TEntity> entities);
        Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
    }
}
