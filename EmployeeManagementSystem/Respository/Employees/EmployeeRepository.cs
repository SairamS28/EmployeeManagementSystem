using EmployeeManagementSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EmployeeManagementSystem.Respository.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeManagementSystemContext _context;

        public EmployeeRepository(EmployeeManagementSystemContext context)
        {
            _context = context;
        }
        public Employee EmployeeLogin(Login employee)
        {
           
            return _context.Employee.Where(e=>e.Email==employee.email && e.Password==employee.password).FirstOrDefault();
        }

        public bool EmployeeRegister(Employee employee)
        {
            var result = _context.Employee.FirstOrDefault(emp=>emp.Email==employee.Email);
            if ( result == null)
            {
                _context.Employee.Add(employee);
                _context.SaveChanges();
            }else
            {
                return false;
            }
           
            return true;
        }

        public bool UpdateEmployee(int id, Employee employee)
        {
     

            try
            {
                _context.Entry(employee).State = EntityState.Modified;
               // _context.Employee.Update(employee);
                _context.SaveChanges();
                return true;
            }catch(System.Exception e)
            {
                return false;
            }
        }

        public Employee ViewEmployee(string email)
        {
            var result= _context.Employee.Include(x => x.Department).Include(y=>y.Manager).FirstOrDefault(y => y.Email == email);
            return result;
        }

        
       
    }
}
