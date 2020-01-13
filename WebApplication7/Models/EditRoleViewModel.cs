using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<String>();
        }
        public String Id { get; set; }
        [Required(ErrorMessage = "Musi byc nazwa")]
        public string RoleName { get; set; }

        public List<String> Users {get; set;}
    }
}
