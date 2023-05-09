namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public interface IFullAuditableEntity : IAuditableEntity, IHasDeletedDate
    {

    }
}
