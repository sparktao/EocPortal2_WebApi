using Hexagon.Data.Repository;
using Hexagon.Entity;
using Hexagon.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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

        public async Task<Organization_Employee> GetEmployeeById(int id)
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
    }
}
