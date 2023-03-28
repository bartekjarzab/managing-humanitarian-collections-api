using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CategoryProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AddProductToCategoryDto> ProductList { get; set; }
    }
}
