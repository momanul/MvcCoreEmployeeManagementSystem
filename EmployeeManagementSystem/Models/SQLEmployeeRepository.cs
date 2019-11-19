using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext dbContext;

        public SQLEmployeeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            dbContext.Add(employee);
            dbContext.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee emp = dbContext.Employees.Find(id);
            dbContext.Remove(emp);
            dbContext.SaveChanges();
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return dbContext.Employees;
        }

        public Employee GetEmployee(int id)
        {
            Employee emp = dbContext.Employees.Find(id);
            return emp;
        }

        public Employee Update(Employee employee)
        {
            var emp = dbContext.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return employee;
        }
    }
}
