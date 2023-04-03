using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int? ProductPropertiesId { get; set; }
        public string ProductCategoryId { get; set; }
      
       // public List<ProductPropertiesDto> ProductProperties { get; set; }
    }
}
