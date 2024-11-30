using Blogpp.data;

namespace Blogpp.Models
{
    public class VMPost
    {
        public List<BlogPostDTO> blogPost { get; set; }
        public List<TopicDTO> topic { get; set; }
    }
}
