using managing_humanitarian_collections_api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api
{
    public class HumanitarianSeeder
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        public HumanitarianSeeder(ManagingCollectionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.ProductCategories.Any())
                {
                    var categories = GetProductCategories();
                  
                    _dbContext.ProductCategories.AddRange(categories);

                    _dbContext.SaveChanges();
                }

                //if (!_dbContext.Product.Any())
                //{
                //    var products = GetProducts();

                //    _dbContext.Product.AddRange(products);

                //    _dbContext.SaveChanges();
                //}
            }

        }

        private IEnumerable<ProductCategory> GetProductCategories()
        {
            var categories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Name = "Ubrania",
                },
                new ProductCategory()
                {
                    Name = "Zabawki",      
 
                },
                new ProductCategory()
                {
                    Name = "Jedzenie",
                },
                new ProductCategory()
                {
                    Name = "Kosmetyki"
                },
            };
            return categories;
        }

        //private IEnumerable<Product> GetProducts()
        //{
        //    var products = new List<Product>()
        //    {
        //        new Product()
        //        {
        //            Name = "Lego",
                    

        //        },
        //        new Product()
        //        {
        //            Name = "Kurtka",
        //            CategoryId = 1,
        //        },
        //        new Product()
        //        {
        //            Name = "czapka",
        //            CategoryId = 1,
        //        },

        //    };
        //    return products;
        
    }
}
