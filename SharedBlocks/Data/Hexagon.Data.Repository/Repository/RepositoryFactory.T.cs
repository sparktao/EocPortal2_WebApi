
using Hexagon.Data.Repository.IRepository;
using Hexagon.Data.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Data.Repository
{
    /// <summary>
    /// 描 述：定义仓储模型工厂
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public class RepositoryFactory<T> where T : class,new()
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        //public IRepositoryAsyn<T> BaseRepositoryAsyn(string connString)
        //{
        //    return new RepositoryAsyn<T>(DbFactory.BaseAsyn(connString, DatabaseType.SQLite));
        //}

        public IRepositoryAsyn<T> BaseRepositoryAsyn(DbContext dbContext)
        {
            return new RepositoryAsyn<T>(DbFactory.BaseAsyn(dbContext, DatabaseType.SQLite));
        }


    }
}
