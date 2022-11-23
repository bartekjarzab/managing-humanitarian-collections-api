namespace managing_humanitarian_collections_api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool DeliveryStatus { get; set; }
        public int Timer { get; set; }

        public int DonatorId { get; set; }
        public int CollectionProductId { get; set; }
       // public virtual Donator Donator { get; set; }
        public virtual CollectionProduct CollectionProduct { get; set; }
    }
}
