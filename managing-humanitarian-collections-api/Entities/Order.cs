using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string DeliveryStatus { get; set; }
        public int? CreatedByDonatorId { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}