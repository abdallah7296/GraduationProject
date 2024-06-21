using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        
       public string MenuName { get; set; }
  
       public List<MenuItem> menuItems { get; set; }
    }
}
