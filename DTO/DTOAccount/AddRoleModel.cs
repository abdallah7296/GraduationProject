using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
