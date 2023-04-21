using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string? Size { get; set; }

        public string ProductCategoryId { get; set; }

        // public List<ProductPropertiesDto> ProductProperties { get; set; }
    }
}
