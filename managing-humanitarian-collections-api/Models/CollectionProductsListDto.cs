namespace managing_humanitarian_collections_api.Models
{
    public class CollectionProductsListDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantily { get; set; }
        public string ShortDescription { get; set; }
        public string ProductName  { get; set; }
    }
}
