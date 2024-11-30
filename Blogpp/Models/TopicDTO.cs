using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    public class TopicDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
