namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        IProductRepository ProductRepository { get; }
        IVariantRepository VariantRepository { get; }
        IOptionValueRepository OptionValueRepository { get; }
        IOptionRepository OptionRepository { get; }
        IVariantOptionValueRepository VariantOptionValueRepository { get; }
    }
}
