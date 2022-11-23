using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int? CreatedById { get; set; }
      //  public virtual Product Products { get; set; }
    }
}
