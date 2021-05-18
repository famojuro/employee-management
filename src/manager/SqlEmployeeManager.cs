using System.Collections.Generic;
using EmployeeManagement.data;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.manager
{
    public class SqlEmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeDataManager _employeeDataManager;
        
        public SqlEmployeeManager(IEmployeeDataManager employeeDataManager)
        {
            _employeeDataManager = employeeDataManager;
        }
        public Employee GetEmployeeDetails(int id)
        {
            var employee = _employeeDataManager.GetDetails(id);

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = _employeeDataManager.GetAll();

            return employees;
        }

        public Employee CreateEmployee(Employee employee)
        {
           var newEmployee = _employeeDataManager.Create(employee);

           return newEmployee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var updatedEmployee = _employeeDataManager.Update(employee);

            return updatedEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            var deletedEmployee = _employeeDataManager.Delete(id);

            return deletedEmployee;
        }
    }
}