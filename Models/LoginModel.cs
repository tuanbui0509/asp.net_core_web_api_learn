using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_learn.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}