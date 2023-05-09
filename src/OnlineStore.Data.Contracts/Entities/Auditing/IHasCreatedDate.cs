namespace OnlineStore.Data.Contracts.Entities.Auditing
{
    public interface IHasCreatedDate
    {
        DateTimeOffset CreatedDate { get; set; }
    }
}
