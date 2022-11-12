using System.ComponentModel.DataAnnotations;

namespace managing_humanitarian_collections_api.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool isOrganizer { get; set; } = false;
    }
}
