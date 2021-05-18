using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.manager
{
    public interface IEmployeeManager
    {
        Employee GetEmployeeDetails(int id);

        IEnumerable<Employee> GetAllEmployees();

        Employee CreateEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        Employee DeleteEmployee(int id);

    }
}