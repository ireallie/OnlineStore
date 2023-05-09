namespace OnlineStore.Data.Contracts.Models
{
    public class ProductParameters : QueryStringParameters
    {
        public ProductParameters()
        {
            OrderBy = "CreatedDate desc";
        }

        public string Name { get; set; }
    }
}
