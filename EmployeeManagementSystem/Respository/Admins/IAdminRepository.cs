using EmployeeManagementSystem.Models;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Respository.Admins
{
    public interface IAdminRepository
    {
     
        void Create(Employee E);
        void Update(int? id,Employee E);
        IEnumerable<Employee> ViewAll();
        Employee ViewById(int? id);
        IEnumerable<Department> GetDepartments();
        IEnumerable<Employee> GetEmployees();
        Admin Login(Admin admin);
        Employee GetDepartment(int? id);
        Employee GetEmployee(int? id);
        bool EmployeeExist(int? id);
    }
}
