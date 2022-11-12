namespace managing_humanitarian_collections_api.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int ContactNumber { get; set; }
        public int CountOfDeliveries { get; set; }
        public int DonatorId { get; set; }
        public int AvatarId { get; set; }

        public virtual Donator Donator { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
}
