using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool DeliveryStatus { get; set; }
        public int ColectionId { get; set; }

        public List<CollectionProduct> CollectionProductsOrder { get; set; }
    }
}
