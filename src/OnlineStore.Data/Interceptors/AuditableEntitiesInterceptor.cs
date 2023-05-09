using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineStore.Data.Contracts.Entities.Auditing;

namespace OnlineStore.Data.Interceptors
{
    public class AuditableEntitiesInterceptor 
        : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var dbContext = eventData.Context;

            if (dbContext == null)
            {
                return base.SavingChanges(eventData, result);
            }

            var insertedEntries = dbContext.ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                var auditableEntity = insertedEntry as IHasCreatedDate;

                if (auditableEntity != null)
                {
                    auditableEntity.CreatedDate = DateTimeOffset.UtcNow;
                }
            }

            var modifiedEntries = dbContext.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                var auditableEntity = modifiedEntry as IHasUpdatedDate;

                if (auditableEntity != null)
                {
                    auditableEntity.UpdatedDate = DateTimeOffset.UtcNow;
                }
            }

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var insertedEntries = dbContext.ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                var hasCreatedDate = insertedEntry as IHasCreatedDate;

                if (hasCreatedDate != null)
                {
                    hasCreatedDate.CreatedDate = DateTimeOffset.UtcNow;
                }
            }

            var modifiedEntries = dbContext.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                var hasUpdatedDate = modifiedEntry as IHasUpdatedDate;

                if (hasUpdatedDate != null)
                {
                    hasUpdatedDate.UpdatedDate = DateTimeOffset.UtcNow;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
