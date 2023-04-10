using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_learn.Models
{
    public class CategoryModel
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }
    }
}