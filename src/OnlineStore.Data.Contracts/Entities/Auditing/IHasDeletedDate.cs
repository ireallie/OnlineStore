namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public interface IHasDeletedDate
    {
        DateTimeOffset? DeletedDate { get; set; }
    }
}
