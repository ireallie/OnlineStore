namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public abstract class FullAuditableEntity : IFullAuditableEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
    }
}
