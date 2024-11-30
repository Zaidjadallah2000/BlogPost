using Microsoft.AspNetCore.Identity;

namespace Blogpp.data
{
    public class ApplicationUser:IdentityUser
    {
        public string name {  get; set; }
        public List<BlogPost> posts { get; set; }   

    }
}
