namespace managing_humanitarian_collections_api.Models
{
    public class AddProductToOrderDto
    {
        public int ProductId { get; set; }
        public int Quantily { get; set; }
        public int CollectionProductId { get; set; }

    }
}
