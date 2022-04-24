using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    [Table("LogonLog")]
    public class LogonLog
    {
        [Key]
        [Required]
        [Display(Name = "Cod")]
        public int LogonLogId { get; set; }
        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; set; }
        [Display(Name = "User name")]
        public User User { get; set; }
        [Required]
        [Display(Name = "Action")]
        public string Action { get; set; }
        [Required]
        [Display(Name = "Creation date")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Log data")]
        public string LogData { get; set; }
    }
}
