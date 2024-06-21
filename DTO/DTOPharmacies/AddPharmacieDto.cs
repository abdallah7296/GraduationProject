using System.ComponentModel.DataAnnotations;
using System;

namespace GraduationProject.DTO.DTOPharmacies
{
    public class AddPharmacieDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(54, ErrorMessage = "The name must be no longer than 54 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(54, ErrorMessage = "The street must be no longer than 54 characters.")]
        public string Street { get; set; }



        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "DescriptionOfPlace is required.")]

        public string DescriptionOfPlace { get; set; }


        [Url(ErrorMessage = "Invalid URL format")]
        public string? LinkOfPlace { get; set; }


        [Required]
        [StringLength(11, ErrorMessage = "The phone number must be no longer than 20 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Start Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartWork { get; set; }

        [Required(ErrorMessage = "End Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndWork { get; set; }

   

        [Range(-90.0, 90.0, ErrorMessage = "The latitude must be between -90 and 90 degrees.")]
        public double Latitude { get; set; }

        [Range(-90.0, 90.0, ErrorMessage = "The latitude must be between -90 and 90 degrees.")]
        public double Longitude { get; set; }
    }
}
