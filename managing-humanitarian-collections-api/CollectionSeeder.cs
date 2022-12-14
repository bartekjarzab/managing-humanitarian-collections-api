using managing_humanitarian_collections_api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api
{
    public class CollectionSeeder
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        public CollectionSeeder(ManagingCollectionsDbContext dbContext)
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
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();

                    _dbContext.Roles.AddRange(roles);

                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Collections.Any())
                {
                    var collections = GetCollections();

                    _dbContext.Collections.AddRange(collections);

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

        private IEnumerable<Collection> GetCollections()
        {
            var collections = new List<Collection>()
            {
                new Collection
                {
                    RegistrationNumber ="11111",
                    Title = "Zbiórka zywnosci",
                    Status = "Otwarta",
                    CollectionPoints = new List<CollectionPoint>()
                    {
                        new CollectionPoint()
                        {
                              Name = "ZHP",
                              OpeningHour = "8:00",
                              ClosingHour = "19:00",
                              Address = new Address()
                              {
                                   Voivodeship ="kujawsko-pomorskie",
                                   Street = "krotka",
                                   City = "Torun",
                                   Postcode = "87-100",
                                   HouseNumber = "68",
                                   Apartment = "2",
                              }
                        },
                        new CollectionPoint()
                        {
                              Name = "ZHP2",
                              OpeningHour = "18:00",
                              ClosingHour = "24:00",
                              Address = new Address()
                              {
                                   Voivodeship ="pomorskie",
                                   Street = "dluga",
                                   City = "Torun",
                                   Postcode = "87-100",
                                   HouseNumber = "5A",
                                   Apartment = "7B",
                              }
                        },
                    }
                },

                new Collection
                {
                    RegistrationNumber ="11111",
                    Title = "Zbiórka ubran",
                    Status = "zamknieta",
                    CollectionPoints = new List<CollectionPoint>()
                    {
                        new CollectionPoint()
                        {
                              Name = "OSP1",
                              OpeningHour = "11:00",
                              ClosingHour = "19:00",
                              Address = new Address()
                              {
                                   Voivodeship ="pomorskie",
                                   Street = "mala",
                                   City = "Torun",
                                   Postcode = "87-100",
                                   HouseNumber = "11",
                                   Apartment = "3",
                              }
                        },
                        new CollectionPoint()
                        {
                              Name = "OSP2",
                              OpeningHour = "12:00",
                              ClosingHour = "24:00",
                              Address = new Address()
                              {
                                   Voivodeship ="pomorskie",
                                   Street = "duza",
                                   City = "Torun",
                                   Postcode = "87-100",
                                   HouseNumber = "13",
                                   Apartment = "2D",
                              }
                        }
                    }
                }
            };

            return collections;
        }
            

    private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Donator",


                },
                new Role()
                {
                    Name = "Organizer",
                },

            };
            return roles;

        }
    }
}
