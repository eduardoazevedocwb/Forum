using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Forum.Models
{
    [Table("TopicLog")]
    public class TopicLog
    {
        [Key]
        [Required]
        [Display(Name = "Cod")]
        public int TopicLogId { get; set; }
        [Required]
        [Display(Name = "TopicId")]
        public int TopicId { get; set; }
        [Required]
        [Display(Name = "Topic")]
        public Topic Topic { get; set; }
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
