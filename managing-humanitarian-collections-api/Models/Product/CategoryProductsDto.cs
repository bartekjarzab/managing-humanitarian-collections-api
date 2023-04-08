using managing_humanitarian_collections_api.Models.Products;
using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Product
{
    public class CategoryProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AddProductToCategoryDto> ProductList { get; set; }
    }
}
