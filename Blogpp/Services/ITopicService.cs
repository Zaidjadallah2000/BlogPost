using Blogpp.data;
using Blogpp.Models;

namespace Blogpp.Services
{
    public interface ITopicService
    {
        //Topic add(TopicDTO topic);
        List<Topic> add(TopicDTO topic);
        void insertBlogTopic(BlogPostTopic topic);
        List<TopicDTO> getTopics();
    }
}