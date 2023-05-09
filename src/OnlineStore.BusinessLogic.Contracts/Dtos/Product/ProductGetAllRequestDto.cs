namespace OnlineStore.BusinessLogic.Contracts.Dtos.Product
{
    public class ProductGetAllRequestDto
    {
        public ProductGetAllRequestDto()
        {
            OrderBy = "CreatedDate desc";
            PageNumber = 1;
            PageSize = 10;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string OrderBy { get; set; }
    }
}
