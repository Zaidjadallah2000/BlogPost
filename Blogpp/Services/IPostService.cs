using Blogpp.data;
using Blogpp.Models;

namespace Blogpp.Services
{
    public interface IPostService
    {
        BlogPost insert(BlogPostDTO postDTO);
        List<BlogPostDTO> getPosts();
        List<BlogPostDTO> Search(string? searchQuery, string? filterTopic);
        void Delete(int id);
        BlogPostDTO Edit(int id);
        void update(BlogPostDTO blogPost);
    }
}