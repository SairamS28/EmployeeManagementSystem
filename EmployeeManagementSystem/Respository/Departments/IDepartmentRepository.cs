using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;


namespace EmployeeManagementSystem.Respository.Departments
{
    public interface IDepartmentRepository
    {
        List<Department> Display();

        Department GetByID(byte id);

        void Update(byte id, Department dept);

        void Create(Department dept);

        


    }
}
