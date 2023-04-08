namespace managing_humanitarian_collections_api.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        #region Darczyńca
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        #endregion

        #region Organizator
        public int? Nip { get; set; }
        public int? Regon { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
        public int? AvatarId { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
}
