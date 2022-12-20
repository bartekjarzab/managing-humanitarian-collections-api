using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool DeliveryStatus { get; set; }
        //  public int ColectionId { get; set; }
        public int? DonatorId { get; set; }

        public virtual User CreatedByDonator { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}