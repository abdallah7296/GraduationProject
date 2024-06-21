using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO.AnalysisCentersDto
{
    public class AddAnalysisCentersDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(54, ErrorMessage = "The name must be no longer than 54 characters.")]
        public string Name { get; set; }

        [StringLength(54, ErrorMessage = "The street must be no longer than 54 characters.")]

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "DescriptionOfPlace is required.")]

        public string DescriptionOfPlace { get; set; }


        [Url(ErrorMessage = "Invalid URL format")]
        public string? LinkOfPlace { get; set; }

 
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Start Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartWork { get; set; }

        [Required(ErrorMessage = "End Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndWork { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double Longitude { get; set; }

    }
}
