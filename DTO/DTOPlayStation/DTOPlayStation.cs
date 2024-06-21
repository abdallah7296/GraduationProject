using GraduationProject.DTO.Images;
using System;
using System.Collections.Generic;

namespace GraduationProject.DTO.DTOPlayStation
{
    public class DTOPlayStation
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string DescriptionOfPlace { get; set; }
        public string LinkOfPlace { get; set; }
        public string PhoneNumber { get; set; }
        public Double PriceOfHour { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public int AvrageRate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<DTOGames> dTOGames { get; set; }
        public List<string> Images { get; set; }
    }
}
