using System.ComponentModel.DataAnnotations;

namespace MyWebApiApp.Models
{
    public class CategoryVM
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }
    }
}