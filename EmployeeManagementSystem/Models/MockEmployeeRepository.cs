using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employees;

        public MockEmployeeRepository()
        {
            employees = new List<Employee>
            {
                new Employee{ID = 1, Name = "Anna", Deparment = Dpt.HR, Email="Anna@gmail.com"},
                new Employee{ID = 2, Name = "Mou", Deparment = Dpt.IT, Email="Mou@gmail.com"},
                new Employee{ID = 3, Name = "Anwar", Deparment = Dpt.IT, Email="Anwar@gmail.com"},
                new Employee{ ID = 4, Name = "Rita", Email = "Rita@gmail.com", Deparment = Dpt.Payroll},
                new Employee{ ID = 5, Name = "Raj", Email = "Raj@gmail.com", Deparment = Dpt.HR}
            };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.ID = employees.Max(e => e.ID) + 1;
            employees.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee emp = employees.FirstOrDefault(i => i.ID == id);
            employees.Remove(emp);
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.ID == id);
        }

        public Employee Update(Employee employee)
        {
            Employee emp = employees.FirstOrDefault(e => e.ID == employee.ID);
            if(emp != null)
            {
                emp.Name = employee.Name;
                emp.Deparment = employee.Deparment;
                emp.Email = employee.Email;
            }
            return emp;
        }
    }
}
