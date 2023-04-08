using managing_humanitarian_collections_api.Models.Order;
using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public string CreatedOrderDate { get; set; }
        public string Status { get; set; }

    }
}