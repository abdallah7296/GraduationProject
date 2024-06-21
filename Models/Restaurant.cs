using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
  
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool HasDelivery { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }


        [Range(-90.0, 90.0, ErrorMessage = "The latitude must be between -90 and 90 degrees.")]
        public double Latitude { get; set; }

        [Range(-180.0, 180.0, ErrorMessage = "The longitude must be between -180 and 180 degrees.")]
        public double Longitude { get; set; }
        [Required]
        public DateTime StartWork { get; set; }

        [Required]
        public DateTime EndWork { get; set; }
    
   
         
        public string DescriptionOfPlace { get; set; }
        [Required]
        public string? LinkOfPlace { get; set; }
        public int? averageRate { get; set; }
        //[ForeignKey("Category")]
       // public int CatId { get; set; }
      //  public Category Category { get; set; }

        [ForeignKey("menu")]
        public int MenuId { get; set; }
        public Menu menu{ get; set; }

      //  public ICollection<CategoryForRestaurant> categoryForRestaurants { get; set; }
      //  public ICollection<Review> Reviews { get; set; }
      

        //public List<Images>? Images { get; }
    }
}
