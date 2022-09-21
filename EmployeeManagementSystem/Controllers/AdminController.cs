using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Respository.Admins;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        IAdminRepository _repo;
        
        public AdminController(IAdminRepository admin)
        {
            _repo = admin;
        
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repo.ViewAll();
            return View(result);
        }
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = _repo.ViewById(id);
            if(result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var employee = _repo.GetEmployees();
            var department = _repo.GetDepartments();
            ViewData["Managerid"] = new SelectList(employee, "Empid", "Name");
            ViewData["Departmentid"] = new SelectList(department, "Departmentid", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee E)
        {
            var employee = _repo.GetEmployees();
            var department = _repo.GetDepartments();
            ViewData["Managerid"] = new SelectList(employee, "Empid", "Name", E.Managerid);
            ViewData["Departmentid"] = new SelectList(department, "Departmentid", "Name", E.Departmentid);
            _repo.Create(E);
            return RedirectToAction("GetAll");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = _repo.GetEmployees();
            var department = _repo.GetDepartments();
            var result = _repo.ViewById(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["Managerid"] = new SelectList(employee, "Empid", "Name", result.Managerid);
            ViewData["Departmentid"] = new SelectList(department, "Departmentid", "Name", result.Departmentid);
            return View(_repo.ViewById(id));
        }
        [HttpPost]
        public IActionResult Edit(int? id,Employee E)
        {
            if (id != E.Empid)
            {
                return NotFound();
            }
            try
            {
                _repo.Update(id, E);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repo.EmployeeExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var employee = _repo.GetEmployees();
            var department = _repo.GetDepartments();
            ViewData["Managerid"] = new SelectList(employee, "Empid", "Name", E.Managerid);
            ViewData["Departmentid"] = new SelectList(department, "Departmentid", "Department Name", E.Departmentid);
            return RedirectToAction("GetAll");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string url)
        {
            //ViewBag.url = url;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                
                if (_repo.Login(admin) != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Sid,admin.Userid.ToString()),
                    new Claim(ClaimTypes.Role,"Admin")

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties()
                    {
                        
                        IsPersistent = false,
                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                        AllowRefresh = true
                    }
                        ) ;
                    HttpContext.Session.SetString("Userid", admin.Userid.ToString());
                    return RedirectToAction("GetAll");

                }
                ModelState.AddModelError(string.Empty, "Invalid username and password");
            }

            return View();

        }

    }
}
