using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class CategoriesDto
    {
        public string Name { get; set; }

        public IList<ProductWithoutPropertiesDto> Products { get; set; }
    }
}
