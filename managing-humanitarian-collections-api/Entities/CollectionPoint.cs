namespace managing_humanitarian_collections_api.Entities
{
    public class CollectionPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public int CollectionId { get; set; }
     //   public int AddressId { get; set; }
        public Address Address { get; set; }
       // public virtual Collection Collection { get; set; }
    }
}
