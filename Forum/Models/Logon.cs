using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    [Table("Logon")]
    public class Logon
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
