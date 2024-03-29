﻿using managing_humanitarian_collections_api.Entities;

namespace managing_humanitarian_collections_api.Models.Collection
{
    public class CollectionPointDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string Voivodeship { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string HouseNumber { get; set; }
        public string Apartment { get; set; }
        public int VoivodeshipId { get; set; }
    }
}
