using Hexagon.Data.Repository;
using Hexagon.Entity;
using Hexagon.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Hexagon.Util.WebControl;
using System.Data;

namespace Hexagon.Service
{
    public class OrgEmployee : RepositoryFactory<Organization_Employee>, IOrgEmployee
    {
        private string conString;
        public OrgEmployee(IConfiguration paramConfiguration)
        {
            if (paramConfiguration != null)
            {
                conString = paramConfiguration.GetConnectionString("OracleConnectionString");
            }
        }

        public async Task<Organization_Employee> GetEmployeeById(long id)
        {
            return await BaseRepositoryAsyn(conString).FindEntityById(id);
        }

        public async Task<List<Organization_Employee>> GetEmployeeList()
        {
            string sql = "select * from Organization_Employee where rownum < 10";
            //string conString = "User Id=cad93;Password=cad93;" +

            //    //How to connect to an Oracle DB without SQL*Net configuration file
            //    //  also known as tnsnames.ora.
            //    "Data Source=192.168.48.30:1521/cad93;";
            return await BaseRepositoryAsyn(conString).FindListBySql(sql);
        }

        public async Task<PaginatedList<Organization_Employee>> GetPagedEmployeeList(Pagination pagination)
        {
            string sqlClause = @"select * from organization_employee";
            return await BaseRepositoryAsyn(conString).FindListPageBySql(sqlClause, pagination);
        }

        public async Task<int> InsertEmployee(Organization_Employee employee)
        {
            string sql = @"insert into organization_employee(
                                employee_id, employee_name, gender, birthday, contact_phone, email, 
                                organization_id, created_date, modified_date, isvalid)
                            values(seq_org_employee.nextval, :employee_name, :gender, :birthday, :contact_phone, :email,
                                  17, sysdate, sysdate, :isvalid)";

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new OracleParameter("employee_name",  employee.Employee_Name));
            parameters.Add(new OracleParameter("gender", employee.Gender));

            parameters.Add(new OracleParameter("birthday", employee.Birthday));
            parameters.Add(new OracleParameter("contact_phone", employee.Contact_Phone));
            parameters.Add(new OracleParameter("email", employee.Email));
            parameters.Add(new OracleParameter("isvalid", employee.Isvalid));

            return await BaseRepositoryAsyn(conString).ExecuteBySql(sql, parameters.ToArray());

        }

        public async Task<int> UpdateEmployee(Organization_Employee employee)
        {
            string sql = string.Format(@"update organization_employee set
                                employee_name = :employee_name, 
                                gender = :gender, 
                                birthday = :birthday,
                                contact_phone = :contact_phone, 
                                email = :email, 
                                modified_date = sysdate,
                                isvalid = :isvalid where employee_id = {0}", employee.Employee_Id);

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new OracleParameter("employee_name", employee.Employee_Name));
            parameters.Add(new OracleParameter("gender", employee.Gender));

            parameters.Add(new OracleParameter("birthday", employee.Birthday));
            parameters.Add(new OracleParameter("contact_phone", employee.Contact_Phone));
            parameters.Add(new OracleParameter("email", employee.Email));
            parameters.Add(new OracleParameter("isvalid", employee.Isvalid));

            return await BaseRepositoryAsyn(conString).ExecuteBySql(sql, parameters.ToArray());

            //return await BaseRepositoryAsyn(conString).Update(employee);

        }
    }
}
