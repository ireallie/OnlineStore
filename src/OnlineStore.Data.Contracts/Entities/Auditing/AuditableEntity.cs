namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
