using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO.DTOReview
{
    public class DTOOReview
    { 
        public int Rating { get; set; }
       public string Comment { get; set; }
       public DateTime ReviewDate { get; set; }

        public string serviceName { get; set; }
    }
}
