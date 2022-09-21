using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Respository.Employees;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repo;
        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employee employee)
        {
            Random r = new Random(); 
            employee.Empid= r.Next(1, 5000);
            if(_repo.EmployeeRegister(employee))
            {
                ViewBag.message = "Registration Successful and this is your Employee ID :"+employee.Empid;
            }
            else
            {
                ViewBag.message = "Email-id already exists";
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login employee)
        {
            var result = _repo.EmployeeLogin(employee);

            if (ModelState.IsValid)
            {

                if (result != null)
                {
                    HttpContext.Session.SetString("email", employee.email);
                    HttpContext.Session.SetString("Empid", result.Empid.ToString());
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email,employee.email),
                    new Claim(ClaimTypes.Role,"Employee")

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties()
                    {
                        IsPersistent = false,
                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                        AllowRefresh = true
                    }
                        );

                    return RedirectToAction("index");
                }
                ModelState.AddModelError(string.Empty, "Invalid username and password");

            }
            return View();
        }

        public IActionResult ViewDetails()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                var employee = _repo.ViewEmployee(HttpContext.Session.GetString("email"));
                return View(employee);
            }
            return RedirectToAction("Login");
        }

        public IActionResult EditDetails()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                var employee = _repo.ViewEmployee(HttpContext.Session.GetString("email"));
                return View(employee);
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public IActionResult UpdateDetails(Employee employee)
        {
            if (HttpContext.Session.GetString("Empid") != null)
            {
                if (_repo.UpdateEmployee(Convert.ToInt32(HttpContext.Session.GetString("Empid")), employee) == false)
                {
                    return RedirectToAction("Login");
                }
                return RedirectToAction("ViewDetails");
            }
            return RedirectToAction("Login");
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldpassword,Employee employee )
        {
            Login login = new Login();
            login.email = HttpContext.Session.GetString("email");
            login.password = oldpassword;
            if (_repo.EmployeeLogin(login) == null)
            {
                ViewBag.message = "Old password is wrong";
            }
            else {
                var emp = _repo.ViewEmployee(HttpContext.Session.GetString("email"));
                emp.Password = employee.Password;
                _repo.UpdateEmployee(Convert.ToInt32(HttpContext.Session.GetString("Empid")), emp);
            }
            return View();
        }

    }

}
