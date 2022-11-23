namespace managing_humanitarian_collections_api.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        //for donator
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? ContactNumber { get; set; }

        //public int CountOfDeliveries { get; set; }

        //for organizer
        public int? Nip { get; set; }
        public int? Regon { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public int? AvatarId { get; set; }

        //public virtual Donator Donator { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
}
