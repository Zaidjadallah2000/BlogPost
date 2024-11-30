using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    public class SignInDTO
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
        public string Password { get; set; }
    }
}
