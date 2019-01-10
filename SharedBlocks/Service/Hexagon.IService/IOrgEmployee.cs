using Hexagon.Entity;
using Hexagon.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.IService
{
    public interface IOrgEmployee
    {

        Task<PaginatedList<Organization_Employee>> GetPagedEmployeeList(Pagination pagination);

        Task<List<Organization_Employee>> GetEmployeeList();

        Task<Organization_Employee> GetEmployeeById(long id);

        Task<int> InsertEmployee(Organization_Employee employee);

        Task<int> UpdateEmployee(Organization_Employee employee);

        Task<int> DeleteEmployee(long employee_id);
    }
}
