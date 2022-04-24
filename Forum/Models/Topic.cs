using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    [Table("Topic")]
    public class Topic
    {
        [Key]
        [Required]
        [Display(Name = "Cod")]
        public int TopicId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Display(Name = "Topic owner")]
        public User User { get; set; }
        [Required]
        [Display(Name = "Last modification")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
