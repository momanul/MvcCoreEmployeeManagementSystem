using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeEditViewModel : HomeCreateViewModel
    {
        public int ID { get; set; }
        public string ExistenceFilePath { get; set; }
    }
}
