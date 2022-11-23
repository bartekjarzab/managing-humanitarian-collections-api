using System.ComponentModel.DataAnnotations;

namespace managing_humanitarian_collections_api.Models.UserModels
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        //    public bool isOrganizer { get; set; } = false;
        public int RoleId { get; set; } = 2;
    }
}
