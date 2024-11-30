using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    public class SignUpDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConPassword { get; set; }
        public string RoleId { get; set; }
    }
}
