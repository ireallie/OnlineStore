using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data.Contracts.Interfaces;

namespace OnlineStore.Data.UOW
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UnitOfWorkFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IUnitOfWork Create()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        }
    }
}
