using Hexagon.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.IService
{
    public interface IOrgEmployee
    {

        Task<List<Organization_Employee>> GetEmployeeList();

        Task<Organization_Employee> GetEmployeeById(int id);

    }
}
