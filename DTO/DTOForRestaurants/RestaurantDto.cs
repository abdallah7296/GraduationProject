using GraduationProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GraduationProject.DTO.Images;
using System;

namespace GraduationProject.DTO.DTOForRestaurants
{
    public class RestaurantDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool HasDelivery { get; set; }
        [Required]
        public string DescriptionOfPlace { get; set; }
        [Required]
        public string? LinkOfPlace { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public int AvrageRate { get; set; }
        public double Longitude { get; set; }
        public DateTime StartWork { get; set; }

        public DateTime EndWork { get; set; }
        [Required]

        //   public List<DTOCategory> dTOCategories { get; set; }
        public List<MenuItemsDto> menuItems { get; set; }
        public List<string> Images { get; set; }

    }
}
