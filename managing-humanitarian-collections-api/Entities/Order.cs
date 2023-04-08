using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public int CollectionId { get; set; }
        public int OrderStatusId { get; set; }

        public string CreatedOrderDate { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual User CreatedBy { get; set; }

    }
}