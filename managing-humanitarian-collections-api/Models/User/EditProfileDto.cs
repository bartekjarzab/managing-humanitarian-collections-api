﻿namespace managing_humanitarian_collections_api.Models.User
{
    public class EditProfileDto
    {

        #region Darczyńca
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion
        #region Organizator
        public long? Nip { get; set; }
        public int? Regon { get; set; }
        public string Name { get; set; }
        public int? AvatarId { get; set; }
        #endregion
        public string ContactNumber { get; set; }

    }
}
