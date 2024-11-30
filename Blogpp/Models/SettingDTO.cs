using System.ComponentModel.DataAnnotations;

namespace Blogpp.Models
{
    public class SettingDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
