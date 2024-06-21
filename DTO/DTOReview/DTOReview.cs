using System.ComponentModel.DataAnnotations;
using System;

namespace GraduationProject.DTO.DTOReview
{
    public class DTOReview
    {
        [Required]
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5 stars.")]
        public int Rating { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The comment must be at least 10 characters long.")]
        public string Comment { get; set; }
      
        public string serviceName { get; set; }

    }
}
