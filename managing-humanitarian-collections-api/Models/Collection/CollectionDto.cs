using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Collection
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public int RegistrationNumber { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }

    }
}
