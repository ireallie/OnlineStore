namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public interface IHasUpdatedDate
    {
        DateTimeOffset? UpdatedDate { get; set; }
    }
}
