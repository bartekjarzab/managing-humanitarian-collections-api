using System;
using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RegistrationNumber { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int? CreatedByOrganiserId { get; set; }
        public virtual List<Order> Orders { get; set; } 
        public virtual User CreatedBy { get; set; }
        public virtual List<CollectionPoint> CollectionPoints { get; set; }
        public virtual List<CollectionProduct> CollectionProducts { get; set; }

    }
}
