using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CollectionWithProductsDto
    {
        public int Id { get; set; }

        public IList<CollectionProductDto> CollectionProducts { get; set; }
    }
}
