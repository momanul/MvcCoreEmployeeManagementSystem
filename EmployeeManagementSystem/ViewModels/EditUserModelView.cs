using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EditUserModelView
    {
        public string ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        public IList<string> UserRoles { get; set; }
        public List<string> UserClaim { get; set; }
    }
}
