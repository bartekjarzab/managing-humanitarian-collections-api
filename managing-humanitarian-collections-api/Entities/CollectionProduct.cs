using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class CollectionProduct
    {
        public int Id { get; set; }
        public int Quantily { get; set; }
        public Product Product { get; set; }
        public int CollectionId { get; set; }


      //  public ICollection<OrderProduct> OrderProducts { get; set; }
        //  public virtual Collection Collection { get; set; }
        //   public virtual Order Order { get; set; }
    }
}
