using Hexagon.Data.Entity;
using Hexagon.Data.Repository;
using Hexagon.IService;
using Microsoft.EntityFrameworkCore;
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
        private readonly DbContext _dbContext;

        public BaseModuleService(IConfiguration paramConfiguration, DbContext dbContext)
        {
            if (paramConfiguration != null)
            {
                conString = paramConfiguration.GetConnectionString("OracleConnectionString");
            }

            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Base_Module>> GetModuleList()
        {
            StringBuilder strSql = new StringBuilder();

            string sql = "select distinct * FROM base_module order by code";
            return await BaseRepositoryAsyn(_dbContext).FindList(sql);
        }

        public async Task<Base_Module> GetModuleById(string id)
        {
            StringBuilder strSql = new StringBuilder();

            return await BaseRepositoryAsyn(_dbContext).FindEntityById(id);
        }
    }
}
