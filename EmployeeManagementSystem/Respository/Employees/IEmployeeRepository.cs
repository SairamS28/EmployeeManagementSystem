using Microsoft.AspNetCore.Mvc.ActionConstraints;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Respository.Employees
{
    public interface IEmployeeRepository
    {
        bool EmployeeRegister(Employee employee);
        Employee EmployeeLogin(Login employee);
        
        Employee ViewEmployee(string email);

        bool UpdateEmployee(int id, Employee employee);


        
    }
}
