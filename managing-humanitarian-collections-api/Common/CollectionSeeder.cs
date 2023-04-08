using managing_humanitarian_collections_api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Common
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
                if (!_dbContext.Avatars.Any())
                {
                    var avatars = GetAvatars();

                    _dbContext.Avatars.AddRange(avatars);

                    _dbContext.SaveChanges();
                }
                if (!_dbContext.CollectionStatuses.Any())
                {
                    var collStatuses = GetColleectionStatuses();

                    _dbContext.CollectionStatuses.AddRange(collStatuses);

                    _dbContext.SaveChanges();
                }
                if (!_dbContext.OrderStatuses.Any())
                {
                    var orderStatuses = GetOrderStatuses();

                    _dbContext.OrderStatuses.AddRange(orderStatuses);

                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();

                    _dbContext.Roles.AddRange(roles);

                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Avatar> GetAvatars()
        {
            var avatars = new List<Avatar>()
            {
                new Avatar()
                {
                    Name = "Kobieta"
                },
                new Avatar()
                {
                    Name = "Mężczyzna",
                },
            };
            return avatars;
        }

        private IEnumerable<CollectionStatus> GetColleectionStatuses()
        {
            var collectionStatuses = new List<CollectionStatus>()
            {
                new CollectionStatus()
                {
                    Status = "Otwarta"
                },
                new CollectionStatus()
                {
                    Status = "Zamknięta",
                },
            };
            return collectionStatuses;
        }

        private IEnumerable<OrderStatus> GetOrderStatuses()
        {
            var collectionStatuses = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Status = "W przygotowaniu"
                },
                new OrderStatus()
                {
                    Status = "Anulowany",
                },
                new OrderStatus()
                {
                    Status = "Dostarczony"
                }
            };
            return collectionStatuses;
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

