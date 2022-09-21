using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Respository.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private EmployeeManagementSystemContext _context;

        public DepartmentRepository(EmployeeManagementSystemContext context)
        {
            _context = context;
        }


        public List<Department> Display()
        {
            return _context.Department.ToList();
            
        }
        public Department GetByID(byte id)
        {
            var result = _context.Department.Find(id);
            return result;

        }
        public void Update(byte id,Department dept)
        {
            _context.Department.Update(dept);
            _context.SaveChanges();
        }
    
        public void Create(Department dept)
        {
            _context.Department.Add(dept);
            _context.SaveChanges();
        }

        //void IDepartmentRepository.Display()
        //{
        //    throw new System.NotImplementedException();
        //}


    }
}
