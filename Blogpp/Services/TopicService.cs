using Blogpp.data;
using Blogpp.Models;

namespace Blogpp.Services
{
    public class TopicService : ITopicService
    {
        private readonly BlogContext context;

        public TopicService(BlogContext _context)
        {
            context = _context;
        }
        //public Topic add(TopicDTO topic)
        //{
        //    var isFind = context.Topics.Where(p=>p.Name == topic.Name).FirstOrDefault();
        //    if (isFind != null)
        //    {
        //        return isFind;
        //    }
        //    else
        //    {
        //        Topic topic1 = new Topic();
        //        topic1.Name = topic.Name;
        //        context.Topics.Add(topic1);
        //        var result = context.SaveChanges();
        //        return topic1;
        //    }

        //}

        public List<Topic> add(TopicDTO topic)
        {
            var topicNames = topic.Name.Split(' ');

            List<Topic> addedTopics = new List<Topic>();

            foreach (var name in topicNames)
            {
                var trimmedName = name.Trim();

                var isFind = context.Topics.Where(p => p.Name == trimmedName).FirstOrDefault();

                if (isFind == null)
                {
                    Topic topic1 = new Topic();
                    topic1.Name = trimmedName;
                    context.Topics.Add(topic1);
                    addedTopics.Add(topic1);
                }
                else
                {
                    addedTopics.Add(isFind);
                }
            }

            context.SaveChanges();
            return addedTopics;
        }

        public void insertBlogTopic(BlogPostTopic topic)
        {
            context.PostTopic.Add(topic);
            context.SaveChanges();
        }
        public List<TopicDTO> getTopics()
        {
           List<Topic> topics = context.Topics.ToList();
            List<TopicDTO> topicDTOs = new List<TopicDTO>();
            foreach (var topic in topics)
            {
                TopicDTO topicDTO = new TopicDTO();
                topicDTO.Id = topic.Id;
                topicDTO.Name = topic.Name;
                topicDTOs.Add(topicDTO);
            }
            return topicDTOs;
        }
    }
}
