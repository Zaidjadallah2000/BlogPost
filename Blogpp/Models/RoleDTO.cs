using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    public class RoleDTO
    {
        public string RoleId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
