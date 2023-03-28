using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        public IList<AddProductToOrderDto> Orders { get; set; }
    }
}
