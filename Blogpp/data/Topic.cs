using System.ComponentModel.DataAnnotations;

namespace Blogpp.data
{
    public class Topic
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        public List<BlogPostTopic> PostTopic { get; set; }
    }
}
