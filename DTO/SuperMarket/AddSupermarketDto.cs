using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO.SuperMarket
{
    public class AddSupermarketDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(54, ErrorMessage = "The name must be no longer than 54 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [StringLength(54, ErrorMessage = "The street must be no longer than 54 characters.")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "DescriptionOfPlace is required.")]

        public string DescriptionOfPlace { get; set; }


     

        [Required(ErrorMessage = "Start Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartWork { get; set; }

        [Required(ErrorMessage = "End Work date is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndWork { get; set; }



        [Required(ErrorMessage = "Latitude is required")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be between -90 and 90 degrees")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be between -180 and 180 degrees")]

        public double Longitude { get; set; }
    
    }
}
