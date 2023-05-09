using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository ProductRepository { get; }
        public IVariantRepository VariantRepository { get; }
        public IOptionValueRepository OptionValueRepository { get; }
        public IOptionRepository OptionRepository { get; }
        public IVariantOptionValueRepository VariantOptionValueRepository { get; }
        public UnitOfWork(
            ApplicationDbContext context,
            IProductRepository productRepository,
            IVariantRepository variantRepository,
            IOptionValueRepository optionValueRepository,
            IOptionRepository optionRepository,
            IVariantOptionValueRepository variantOptionValueRepository)
        {
            _context = context;

            ProductRepository = productRepository;
            VariantRepository = variantRepository;
            OptionValueRepository = optionValueRepository;
            OptionRepository = optionRepository;
            VariantOptionValueRepository = variantOptionValueRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
