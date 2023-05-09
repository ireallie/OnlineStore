namespace OnlineStore.BusinessLogic.Contracts.Dtos
{
    public interface IAuditedEntityDto 
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
