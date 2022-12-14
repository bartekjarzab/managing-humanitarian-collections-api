using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CollectionWithAddressDto
    {
        public string RegistrationNumber { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public IList<CollectionPointDto> CollectionPoints { get; set; }

    }
}
