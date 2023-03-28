using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Collection
{
    public class CollectionWithProductsDto
    {
        public int Id { get; set; }

        public IList<CreateCollectionProductDto> CollectionProducts { get; set; }
    }
}
