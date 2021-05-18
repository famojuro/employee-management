using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.data
{
    public interface IEmployeeDataManager
    {
        Employee GetDetails(int id);

        IEnumerable<Employee> GetAll();

        Employee Create(Employee employee);

        Employee Update(Employee employee);

        void Delete(int id);
    }
}