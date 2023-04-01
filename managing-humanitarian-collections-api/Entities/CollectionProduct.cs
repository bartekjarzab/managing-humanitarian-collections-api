using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class CollectionProduct
    {
        public int Id { get; set; }
        public int Quantily { get; set; }
        public int ProductId { get; set; }
        public string ShortDescription { get; set; }
        public Product Product { get; set; }
        public int CollectionId { get; set; }


    }
}
