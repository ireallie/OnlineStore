namespace OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption.Base
{
    public class ProductOptionBaseDto
    {
        public string Name { get; set; }
        public IList<string> Values { get; set; } = new List<string>();
    }
}
