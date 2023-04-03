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
                if (!_dbContext.Voivodeships.Any())
                {
                    var voivodeships = GetVoivodeships();

                    _dbContext.Voivodeships.AddRange(voivodeships);

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

               
            }
        }


        private IEnumerable<Voivodeship> GetVoivodeships()
        {
            var voivodeships = new List<Voivodeship>()
            {
                new Voivodeship()
                {
                    Name = "dolnośląskie"
                },
                new Voivodeship()
                {
                    Name = "kujawsko-pomorskie",
                },
                new Voivodeship()
                {
                    Name = "lubelskie",
                },
                new Voivodeship()
                {
                    Name = "lubuskie",
                },
                new Voivodeship()
                {
                    Name = "łódzkie",
                },
                new Voivodeship()
                {
                    Name = "małopolskie",
                },
                new Voivodeship()
                {
                    Name = "mazowieckie",
                },
                new Voivodeship()
                {
                    Name = "opolskie",
                },
                new Voivodeship()
                {
                    Name = "podkarpackie",
                },
                new Voivodeship()
                {
                    Name = "podlaskie",
                },
                new Voivodeship()
                {
                    Name = "pomorskie",
                },
                new Voivodeship()
                {
                    Name = "śląskie",
                },
                new Voivodeship()
                {
                    Name = "świętokrzyskie",
                },
                new Voivodeship()
                {
                    Name = "warmińsko-mazurskie",
                },
                new Voivodeship()
                {
                    Name = "wielkopolskie",
                },
                new Voivodeship()
                {
                    Name = "zachodniopomorskie",
                },

            };
            return voivodeships;

        }


        private IEnumerable<ProductCategory> GetProductCategories()
        {
            var categories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Name = "Odzież",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Kurtka",
                        },
                        new Product()
                        {
                            Name = "Ubuwie",
                        },
                        new Product()
                        {
                            Name = "Koszulka",
                        },
                        new Product()
                        {
                            Name = "Spodnie",
                        }, 
                        new Product()
                        {
                            Name = "Bielizna",
                        }, 
                        new Product()
                        {
                            Name = "Czapka",
                        },                    
                    }
                },
                new ProductCategory()
                {
                    Name = "Zabawki",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Piłka",
                        },
                        new Product()
                        {
                            Name = "Rower",
                        },
                        new Product()
                        {
                            Name = "Hulajnoga",
                        },
                        new Product()
                        {
                            Name = "Klocki",
                        },
                        new Product()
                        {
                            Name = "Latawiec",
                        },
                        new Product()
                        {
                            Name = "Rakieta tenisowa",
                        },
                    }

                },
                new ProductCategory()
                {
                    Name = "Żywność",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Makaron",
                        },
                        new Product()
                        {
                            Name = "Mleko",
                        },
                        new Product()
                        {
                            Name = "Cukier",
                        },
                        new Product()
                        {
                            Name = "Sól",
                        },
                        new Product()
                        {
                            Name = "Masło",
                        },
                        new Product()
                        {
                            Name = "Olej",
                        },
                         new Product()
                        {
                            Name = "Woda niegazowana",
                        },
                    }
                },
                new ProductCategory()
                {
                    Name = "Higiena",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Papier toaletowy",
                        },
                        new Product()
                        {
                            Name = "Mydło",
                        },
                        new Product()
                        {
                            Name = "Szampon",
                        },
                        new Product()
                        {
                            Name = "Ręcznik",
                        },
                        new Product()
                        {
                            Name = "Podpaski",
                        },
                        new Product()
                        {
                            Name = "Ręcznik papierowy",
                        },
                    }
                },
                 new ProductCategory()
                {
                    Name = "Artkuły domowe",
                     Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Garnek",
                        },
                        new Product()
                        {
                            Name = "Sztućce",
                        },
                        new Product()
                        {
                            Name = "Talerz",
                        },
                        new Product()
                        {
                            Name = "Miska",
                        },
                        new Product()
                        {
                            Name = "Kubek",
                        },
                        new Product()
                        {
                            Name = "Patelnia",
                        },
                    }
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
                    RegistrationNumber = 11111,
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

                                   VoivodeshipId = 1,
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
                                   VoivodeshipId = 1,
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
                    RegistrationNumber = 1113411,
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
                                   VoivodeshipId = 1,
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

                                   VoivodeshipId = 1,
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
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "Darczyńca",
                },
                new Role()
                {
                    Name = "Organizator",
                },

            };
            return roles;

        }

       
    }
    
}

