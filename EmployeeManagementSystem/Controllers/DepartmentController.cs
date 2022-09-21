using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Respository.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _repo;

        public DepartmentController(IDepartmentRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Display()
        {
            return View(_repo.Display());
        }
        public IActionResult GetByID(byte id)
        {
            _repo.GetByID(id);
            return View(_repo.GetByID(id));
        }

        public IActionResult Update(byte id)
        {
            return View(_repo.GetByID(id));
        }
    
        [HttpPost]
        public IActionResult Update(byte id, Department dept)
        {
            _repo.Update(id,dept);
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            _repo.Create(dept);
            return View();
        }


    }
}
