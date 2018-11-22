using Hexagon.Data.Repository;
using Hexagon.Entity;
using Hexagon.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Service
{
    public class BaseModuleService : RepositoryFactory<Base_Module>, IBaseModuleService
    {
        private string conString;

        public BaseModuleService(IConfiguration paramConfiguration)
        {
            if (paramConfiguration != null)
            {
                conString = paramConfiguration.GetConnectionString("OracleConnectionString");
            }
        }
        public async Task<List<Base_Module>> GetModuleList()
        {
            StringBuilder strSql = new StringBuilder();

            string sql = "select distinct * FROM base_module order by code";
            return await BaseRepositoryAsyn(conString).FindListBySql(sql);
        }
    }
}
