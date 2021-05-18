using System.Collections.Generic;
using EmployeeManagement.data;
using EmployeeManagement.Models;

namespace EmployeeManagement.manager
{
    public class MockEmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeDataManager _mockEmployeeDataManager;
        
        public MockEmployeeManager(IEmployeeDataManager mockEmployeeDataManager)
        {
            _mockEmployeeDataManager = mockEmployeeDataManager;
        }
        
        public Employee GetEmployeeDetails(int id)
        {
            Employee employee = _mockEmployeeDataManager.GetDetails(id);

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employeeList = _mockEmployeeDataManager.GetAll();

            return employeeList;
        }

        public Employee CreateEmployee(Employee employee)
        {
            Employee newEmployee = _mockEmployeeDataManager.Create(employee);

            return newEmployee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            Employee updatedEmployee = _mockEmployeeDataManager.Update(employee);

            return updatedEmployee;
        }

        public void DeleteEmployee(int id)
        {
            _mockEmployeeDataManager.Delete(id); ;
        }
    }
}