using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5 stars.")]
        public int Rating { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The comment must be at least 10 characters long.")]
        public string Comment { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

 
        public int ServicId { get; set; } 
        public  string serviceName { get; set; }

        [ForeignKey("user")]
        public string? UserId { get; set; } 
        public User user { get; set; }
 

    }
}
