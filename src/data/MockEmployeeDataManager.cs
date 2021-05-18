using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Models;

namespace EmployeeManagement.data
{
    public class MockEmployeeDataManager : IEmployeeDataManager
    {
        private readonly List<Employee> _employeeList;
        
        public MockEmployeeDataManager()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Alfred Richard", Email = "alfredrichard@gmail.com", Department = Department.HR},
                new Employee() {Id = 2, Name = "Goke Biodun", Email = "biodun1@gmail.com", Department = Department.IT},
                new Employee() {Id = 3, Name = "Mary Janne", Email = "maryjanne@gmail.com", Department = Department.Payroll},
            };
        }
        
        public Employee GetDetails(int id)
        {
            return _employeeList.FirstOrDefault(employee => employee.Id.Equals(id));
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeList;
        }

        public Employee Create(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
             _employeeList.Add(employee);

             return employee;
        }

        public Employee Update(Employee employee)
        {
            Employee updatedEmployee = _employeeList.FirstOrDefault(e => e.Id.Equals(employee.Id));
            if (updatedEmployee != null)
            {
                updatedEmployee.Name = employee.Name;
                updatedEmployee.Email = employee.Email;
                updatedEmployee.Department = employee.Department;
            }

            return updatedEmployee;
        }

        public void Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id.Equals(id));
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
        }
    }
}