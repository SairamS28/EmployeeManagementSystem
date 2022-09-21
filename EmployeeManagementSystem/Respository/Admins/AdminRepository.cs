using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Respository.Admins
{
    public class AdminRepository : IAdminRepository
    {
        private EmployeeManagementSystemContext _context;

        public AdminRepository(EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        public void Create(Employee E)
        {
            _context.Employee.Add(E);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> ViewAll()
        {
            return _context.Employee.Include(x=>x.Department).Include(y=>y.Manager).ToList();
        }

        public Employee ViewById(int? id)
        {
           
            var result = _context.Employee.Include(x=>x.Manager).ThenInclude(y=>y.Department).FirstOrDefault(z=>z.Empid==id);
            return result;

        }

        public void Update(int? id,Employee E)
        {
                _context.Employee.Update(E);
                _context.SaveChanges();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Department;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employee;
        }
        public Admin Login(Admin admin)
        {
            return _context.Admin.FirstOrDefault(x => x.Userid == admin.Userid && x.Password == admin.Password);
        }

        public Employee GetDepartment(int? id)
        {
            return _context.Employee.Include(x => x.Department).FirstOrDefault(y => y.Empid == id);
        }

        public Employee GetEmployee(int? id)
        {
            return _context.Employee.Include(x => x.Manager).FirstOrDefault(y => y.Empid == id);
        }
        public bool EmployeeExist(int? id)
        {
            return _context.Employee.Any(e => e.Empid == id);
        }


    }
}
