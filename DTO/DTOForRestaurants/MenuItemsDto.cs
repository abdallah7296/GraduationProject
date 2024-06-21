using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO.DTOForRestaurants
{
    public class MenuItemsDto
    {
        public int id { get; set; }
         public string Name { get; set; }
        public string Taste { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string size { get; set; }
        public string TypeOfMeal { get; set; }
    }
}
