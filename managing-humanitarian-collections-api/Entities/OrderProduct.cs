namespace managing_humanitarian_collections_api.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantily { get; set; }
        public int CollectionProductId { get; set; }
        public CollectionProduct CollectionProduct { get; set; }
       // public int OrderId { get; set; }
       // public Order Order { get; set; }
    }
}
