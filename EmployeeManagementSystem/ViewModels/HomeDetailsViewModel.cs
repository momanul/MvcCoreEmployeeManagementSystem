using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class HomeDetailsViewModel
    {
        public int ID { get; set; }
        public Employee Employee { get; set; }
        public string PageTittle { get; set; }
    }
}
