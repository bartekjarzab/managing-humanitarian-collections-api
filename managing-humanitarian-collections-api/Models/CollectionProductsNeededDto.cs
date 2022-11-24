using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CollectionProductsNeededDto
    {
        public int CollectionId { get; set; } 
        public IList <CollectionProductDto> Products { get; set; }
    }
}
