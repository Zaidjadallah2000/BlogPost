using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogpp.data
{
    public class BlogPostTopic
    {
        [Key]
        [ForeignKey("blogpost")]
        public int BlogPostId { get; set; }
        [Key]
        [ForeignKey("topic")]
        public int TopicId { get; set; }

        public BlogPost blogpost { get; set; }
        public Topic topic { get; set; }
       
    }
}
