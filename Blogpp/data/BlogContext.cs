using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogpp.data
{
    public class BlogContext:IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration config;

        public BlogContext(IConfiguration _config)
        {
            config = _config;
        }
        public DbSet<ApplicationUser> applicationUsers {  get; set; }
        public DbSet<BlogPost> Posts {  get; set; }
        public DbSet<Topic> Topics {  get; set; }
        public DbSet<BlogPostTopic> PostTopic {  get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("conn"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogPostTopic>().HasKey(e=>new {e.BlogPostId, e.TopicId});
            base.OnModelCreating(builder);
        }
    }
}
