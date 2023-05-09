namespace OnlineStore.BusinessLogic.Contracts.Dtos
{
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IEntityDto : IEntityDto<int>
    {
    }
}
