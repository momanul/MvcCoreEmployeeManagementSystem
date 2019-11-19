using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class RoleEditViewModel
    {
        //To Avoid null reference execption of list<string> RoleMembers
        public RoleEditViewModel()
        {
            RoleMembers = new List<string>();
        }
        public string ID { get; set; }
        public string RoleName { get; set; }
        public List<string> RoleMembers { get; set; }
    }
}
