using System.Collections.Generic;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.data
{
    public class SqlEmployeeDataManager : IEmployeeDataManager
    {
        private readonly AppDbContext _context;
        
        public SqlEmployeeDataManager(AppDbContext context)
        {
            _context = context;
        }
        public Employee GetDetails(int id)
        {
            var employee = _context.Employees.Find(id);

            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _context.Employees;

            return employees;
        }

        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public Employee Update(Employee employee)
        {
            var updatedEmployee = _context.Employees.Attach(employee);
            updatedEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return employee;
        }

        public Employee Delete(int id)
        {
           Employee employee = _context.Employees.Find(id);
           if (employee != null)
           {
               _context.Employees.Remove(employee);
               _context.SaveChanges();
           }

           return employee;
        }
    }
}