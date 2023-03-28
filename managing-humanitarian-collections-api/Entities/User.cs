namespace managing_humanitarian_collections_api.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string HashPassword { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Profile Profile { get; set; }
     
    }
}
