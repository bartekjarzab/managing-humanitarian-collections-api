using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CollectionWithAdressDto
    {
        public CollectionWithAdressDto(CollectionDto collection, List<CollectionPointDto> collectionPoints)
        {
            Collection = collection;
            CollectionPoint = collectionPoints;
        }
        public CollectionDto Collection { get; set; }
        List<CollectionPointDto> CollectionPoint { get; set; }
        
    }
}
