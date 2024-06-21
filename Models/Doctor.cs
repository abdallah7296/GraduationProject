using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Doctor  
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The name must be no longer than 255 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The street address must be no longer than 255 characters.")]
        public string Street { get; set; }


        [Required]
        [StringLength(255, ErrorMessage = "The city must be no longer than 255 characters.")]
        public string City { get; set; }


        [Required]
        [StringLength(11, ErrorMessage = "The phone number must be no longer than 20 characters.")]
        public string PhoneNumber { get; set; }

        [Required] 
        public DateTime StartWork { get; set; }

        [Required]
        public DateTime EndWork { get; set; }
        [Required]
        public string DescriptionOfPlace { get; set; }
        [Required]
        public string? LinkOfPlace { get; set; }
        [Range(-90.0, 90.0, ErrorMessage = "The latitude must be between -90 and 90 degrees.")]
        public double Latitude { get; set; }

        [Range(-90.0, 90.0, ErrorMessage = "The latitude must be between -90 and 90 degrees.")]
        public double Longitude { get; set; }
        public string  Specialization { get; set; }

      //  [ForeignKey("Category")]
       // public int CatId { get; set; }
      //  public Category Category { get; set; }
        // public List<Images>? Images { get; set; }
        // public ICollection<Review> Reviews { get; set; }
        public int? averageRate { get; set; }
        

    }
}
