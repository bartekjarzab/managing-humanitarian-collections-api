using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class OrderWithCollectionDto
    {
        public int Id { get; set; }
        public IList<OrderProductDto> OrderProducts { get; set; }
    }
}
