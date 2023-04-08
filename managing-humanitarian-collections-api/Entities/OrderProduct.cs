namespace managing_humanitarian_collections_api.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantily { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
    
    }
}
