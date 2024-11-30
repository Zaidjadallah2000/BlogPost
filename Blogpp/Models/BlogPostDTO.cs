using AutoMapper;
using Blogpp.data;
using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    [AutoMap(typeof(BlogPost),ReverseMap =true)]
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Topic { get; set; }
        public string ImageURL { get; set; }
        public IFormFile? file { get; set; }
        public DateTime? PostDate { get; set; }
    }
}
