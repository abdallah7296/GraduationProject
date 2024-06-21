using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Images
    {

        public int Id { get; set; }
        public string Image { get; set; }

        public int ServicId { get; set; }
        public string serviceName { get; set; }



    }
}
