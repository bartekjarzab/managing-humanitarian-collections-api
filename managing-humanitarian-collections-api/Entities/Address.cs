namespace managing_humanitarian_collections_api.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int VoivodeshipId { get; set; }
        public string? Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string? HouseNumber { get; set; }
        public string? Apartment { get; set; } 
        public int CollectionPointId { get; set; }
        public Voivodeship Voivodeship { get; set; }
    }
}
