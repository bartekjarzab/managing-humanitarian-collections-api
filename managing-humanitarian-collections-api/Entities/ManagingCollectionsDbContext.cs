using Microsoft.EntityFrameworkCore;

namespace managing_humanitarian_collections_api.Entities
{
    public class ManagingCollectionsDbContext : DbContext
    {
       
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionPoint> CollectionPoints { get; set; }
        public DbSet<CollectionProduct> CollectionProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Donator> Donators { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProperties> ProductPropertiess { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelbuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ManagementCollections;Trusted_Connection=True;");
        }
    }
}
