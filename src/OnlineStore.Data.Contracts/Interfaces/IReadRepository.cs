namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class { }
}
