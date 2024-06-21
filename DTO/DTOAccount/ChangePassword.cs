using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTO
{
    public class ChangePassword
    {
       
        public string UserName { get; set; }
  
        public string OldPassword { get; set; }
    
        public string NewPassword { get; set; }
    
        public string ConfirmNewPassword { get; set; }

    }
}
