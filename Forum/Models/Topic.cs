using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Display(Name = "Topic owner")]
        public User User { get; set; }
        [Required]
        [Display(Name = "Modification")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Deleted")]
        public bool? Deleted { get; set; }
    }
}
