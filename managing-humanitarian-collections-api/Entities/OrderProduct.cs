namespace managing_humanitarian_collections_api.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantily { get; set; }
        public int ProductId { get; set; }
         //public virtual CollectionProduct CollectionProduct { get; set; }
        //public int CollectionProductId { get; set; }
        public int OrderId { get; set; }
    
    }
}
