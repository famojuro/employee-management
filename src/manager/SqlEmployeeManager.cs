using System.Collections.Generic;
using EmployeeManagement.data;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.manager
{
    public class SqlEmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeDataManager _employeeDataManager;
        private readonly ILogger<SqlEmployeeManager> _logger;

        public SqlEmployeeManager(IEmployeeDataManager employeeDataManager,
            ILogger<SqlEmployeeManager> logger)
        {
            _employeeDataManager = employeeDataManager;
            _logger = logger;
        }
        public Employee GetEmployeeDetails(int id)
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Information log");
            _logger.LogWarning("Warning log");
            _logger.LogError("Error log");
            _logger.LogCritical("Critical log");
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

        public void DeleteEmployee(int id)
        {
            _employeeDataManager.Delete(id);
        }
    }
}