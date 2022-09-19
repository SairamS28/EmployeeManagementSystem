using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmployeeManagementSystem.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
        }

        public int Empid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte? Departmentid { get; set; }
        public string Address { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        public int? Managerid { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
