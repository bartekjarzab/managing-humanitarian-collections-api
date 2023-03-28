using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Collection
{
    public class CollectionProductsNeededDto
    {
        public int CollectionId { get; set; }
        public IList<CreateCollectionProductDto> Products { get; set; }
    }
}
