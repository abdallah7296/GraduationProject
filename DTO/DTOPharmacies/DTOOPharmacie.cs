using System.ComponentModel.DataAnnotations;
using System;
using GraduationProject.DTO.Images;
using System.Collections.Generic;

namespace GraduationProject.DTO.DTOPharmacies
{
    public class DTOOPharmacie
    {
        public int id { get; set; }

        public string Name { get; set; }
         public string Street { get; set; }

         public string City { get; set; }
        public int AvrageRate { get; set; }
        public string PhoneNumber { get; set; }

         public DateTime StartWork { get; set; }

         public DateTime EndWork { get; set; }
         public string DescriptionOfPlace { get; set; }
         public string? LinkOfPlace { get; set; }
 
         public double Latitude { get; set; }

         public double Longitude { get; set; }
        public List<string> Images { get; set; }
    }
}
