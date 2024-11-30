using System.ComponentModel.DataAnnotations.Schema;

namespace Blogpp.data
{
    public class BlogPost
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Topic { get; set; }
        public DateTime PostDate { get; set; }
        public string ImageURL { get; set; }
        public List<BlogPostTopic> PostTopic { get; set; }

    }
}
