using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The name must be no longer than 255 characters.")]
        public string Name { get; set; }


        [StringLength(255, ErrorMessage = "The taste must be no longer than 255 characters.")]
        public string Taste { get; set; }


        [StringLength(1024, ErrorMessage = "The description must be no longer than 1024 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The price must be a positive number.")]
        public int Price { get; set; }

        [Required]
        public string size { get; set; }

        public string  TypeOfMeal {  get; set; }

        [ForeignKey("menu")]
        public int MenuId { get; set; }
        public Menu menu { get; set; }

       

    }
}
