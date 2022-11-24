using managing_humanitarian_collections_api.Entities;

namespace managing_humanitarian_collections_api.Models
{
    public class CollectionPointDto
    {
        public string Name { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string Voivodeship { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string HouseNumber { get; set; }
        public string Apartment { get; set; }
        //public AddressDto Address { get; set; }
    } 
}
