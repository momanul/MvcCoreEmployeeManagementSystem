using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public static class EmployeeSeedDataModel
    {
        public static void Seed(this ModelBuilder model)
        {
            model.Entity<Employee>().HasData(
                 new Employee { ID = 1, Name = "Anna", Deparment = Dpt.HR, Email = "Anna@gmail.com" },
                new Employee { ID = 2, Name = "Mou", Deparment = Dpt.IT, Email = "Mou@gmail.com" },
                new Employee { ID = 3, Name = "Anwar", Deparment = Dpt.IT, Email = "Anwar@gmail.com" },
                new Employee { ID = 4, Name = "Rita", Email = "Rita@gmail.com", Deparment = Dpt.Payroll },
                new Employee { ID = 5, Name = "Raj", Email = "Raj@gmail.com", Deparment = Dpt.HR }
            );
        }
    }
}
