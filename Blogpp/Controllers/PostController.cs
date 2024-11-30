using Blogpp.data;
using Blogpp.Models;
using Blogpp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Blogpp.Controllers
{
    public class PostController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IPostService postService;
        private readonly ITopicService topicService;

        public PostController(IHostingEnvironment _host,
            IPostService _postService,
            ITopicService _topicService
            )
        {
            host = _host;
            postService = _postService;
            topicService = _topicService;
        }
        public IActionResult Index()
        {
            VMPost post = new VMPost();
            post.blogPost = postService.getPosts();
            post.topic = topicService.getTopics();
            return View("index",post);
        }
        [Authorize]
        public IActionResult create()
        {
            ViewData["IsEdit"] = false;
            return View("create");
        }
        [Authorize]
        public  IActionResult AddPost(BlogPostDTO postDTO)
        {
           
            string fileName = string.Empty;
            if (postDTO.file != null)
            {
                string uploads = Path.Combine(host.WebRootPath, "Uploads");
                fileName = postDTO.file.FileName;
                string fullpath = Path.Combine(uploads, fileName);
                 postDTO.file.CopyToAsync(new FileStream(fullpath, FileMode.Create));

            }
              var blog = postService.insert(postDTO);
             TopicDTO topicDTO = new TopicDTO();
             topicDTO.Name = postDTO.Topic;
             var Topic = topicService.add(topicDTO);
            foreach (var item in Topic)
            {
                BlogPostTopic blogPostTopic = new BlogPostTopic();
                blogPostTopic.blogpost = blog;
                blogPostTopic.topic = item;
                topicService.insertBlogTopic(blogPostTopic);
            }
            


            return Redirect("Index");
        }


        public IActionResult SearchPosts(string? searchQuery, string? filterTopic)
        {
           
            VMPost post = new VMPost();
            post.blogPost = postService.Search(searchQuery, filterTopic);
            post.topic = topicService.getTopics();
            return View("index", post);
        }

        public IActionResult deletePost(int id)
        {
            try
            {
                postService.Delete(id);
                TempData["Message"] = "Post deleted successfully!";
                TempData["MessageType"] = "success"; // يمكنك تحديد نوع الرسالة (success, error, warning)
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while deleting the post.";
                TempData["MessageType"] = "error"; // نوع الرسالة في حالة وجود خطأ
            }
            return Redirect("index");
        }

        
        public IActionResult EditPost(int id)
        {
            ViewData["IsEdit"] = true;
            BlogPostDTO blog =  postService.Edit(id);
            return View("create",blog);
        }

        public IActionResult update(BlogPostDTO blog)
        {
            try{
                string fileName = string.Empty;
                if (blog.file != null)
                {
                    string uploads = Path.Combine(host.WebRootPath, "Uploads");
                    fileName = blog.file.FileName;
                    string fullpath = Path.Combine(uploads, fileName);
                    blog.file.CopyToAsync(new FileStream(fullpath, FileMode.Create));

                }
                postService.update(blog);
                TempData["Message"] = "Post Updated successfully!";
                TempData["MessageType"] = "success";
            }
            catch(Exception e)
            {
                TempData["Message"] = "An error occurred while deleting the post.";
                TempData["MessageType"] = "error";
            }
            return Redirect("index");
            
        }


   }
}
