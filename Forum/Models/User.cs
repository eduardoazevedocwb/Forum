using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        [Display(Name = "Cod")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Deleted")]
        public bool? Deleted { get; set; }
    }
}
