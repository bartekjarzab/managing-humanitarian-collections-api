namespace managing_humanitarian_collections_api.Models.Admin
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        #region Darczyńca
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion



        #region Organizator
        public int? Nip { get; set; }
        public int? Regon { get; set; }
        public string Name { get; set; }
        public string? Avatar { get; set; }
        #endregion
        public string ContactNumber { get; set; }
        public string Role { get; set; }

    }
}
