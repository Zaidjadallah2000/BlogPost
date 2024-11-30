using AutoMapper;
using Blogpp.data;
using Blogpp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
namespace Blogpp.Services
{
    public class PostService : IPostService
    {
        private readonly BlogContext context;
        private readonly IMapper mapper;

        public PostService(BlogContext _context,
            IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public BlogPost insert(BlogPostDTO postDTO)
        {
            BlogPost blogPost = new BlogPost();
            blogPost.Id = postDTO.Id;
            blogPost.UserId = postDTO.UserId;
            blogPost.Title = postDTO.Title;
            blogPost.Content = postDTO.Content;
            blogPost.Topic = postDTO.Topic;
            blogPost.ImageURL = postDTO.file.FileName;
            blogPost.PostDate = DateTime.Now;
            context.Posts.Add(blogPost);
            context.SaveChanges();
            return blogPost;
        }

        public List<BlogPostDTO> getPosts()
        {
            List<BlogPost> blogPosts = context.Posts.Include("User").OrderByDescending(p=>p.PostDate).ToList();
            List<BlogPostDTO> blogs = new List<BlogPostDTO>();
            foreach (var item in blogPosts)
            {
                BlogPostDTO blogPostDTO = new BlogPostDTO();
                blogPostDTO.Id = item.Id;
                blogPostDTO.UserId = item.UserId;
                blogPostDTO.Title = item.Title;
                blogPostDTO.Content = item.Content;
                blogPostDTO.Topic = item.Topic;
                blogPostDTO.ImageURL = item.ImageURL;
                blogPostDTO.PostDate = item.PostDate;
                blogs.Add(blogPostDTO);


            }
            return blogs;

        }

        public List<BlogPostDTO> Search(string? searchQuery, string? filterTopic)
        {
            // إنشاء استعلام مبدئي
            var query = context.Posts.Include("User").OrderByDescending(p => p.PostDate).AsQueryable();

            // تطبيق فلتر البحث عن الاسم إذا كان موجودًا
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.User.name.Contains(searchQuery));
            }

            // تطبيق فلتر الموضوع إذا كان موجودًا
            if (!string.IsNullOrEmpty(filterTopic))
            {
                query = query.Where(p => p.Topic == filterTopic);
            }

            // تحويل النتائج إلى قائمة من BlogPostDTO
            var blogs = query.Select(item => new BlogPostDTO
            {
                Id = item.Id,
                UserId = item.UserId,
                Title = item.Title,
                Content = item.Content,
                Topic = item.Topic,
                ImageURL = item.ImageURL,
                PostDate = item.PostDate
            }).ToList();

            return blogs;
        }

        public void Delete(int id)
        {
            BlogPost blog = context.Posts.Find(id);
            if (!string.IsNullOrEmpty(blog.ImageURL))
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", blog.ImageURL);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            context.Posts.Remove(blog);
            context.SaveChanges();
        }


        public BlogPostDTO Edit(int id)
        {
            
            BlogPost blog = context.Posts.Find(id);
            BlogPostDTO blogPost = new BlogPostDTO();
           
            blogPost.Id = blog.Id;
            blogPost.UserId = blog.UserId;
            blogPost.Title = blog.Title;
            blogPost.Content = blog.Content;
            blogPost.Topic = blog.Topic;
            blogPost.ImageURL = blog.ImageURL;
            blogPost.PostDate = blog.PostDate;
            return blogPost;
        }


        public void update(BlogPostDTO blogPost)
        {
            BlogPost blog = new BlogPost();
            blog.Id = blogPost.Id;
            blog.UserId = blogPost.UserId;
            blog.Title = blogPost.Title;
            blog.Content = blogPost.Content;
            blog.Topic = blogPost.Topic;
            blog.PostDate = (DateTime)blogPost.PostDate;
            if (blogPost.file != null)
            {
                blog.ImageURL = blogPost.file.FileName;
            }
            else
            {
                blog.ImageURL = blogPost.ImageURL;
            }
           
            context.Posts.Attach(blog);
            context.Entry(blog).State = EntityState.Modified;
            context.SaveChanges();
        }
    
    
    }
}
