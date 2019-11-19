using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Gender? Gender { get; set; }
    }
}
