using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Login
    {
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
