using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Games
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        [ForeignKey("PlayStation")]
        public int PlayStationId { get; set; }
        public PlayStation PlayStation { get; set; }
    }
}
