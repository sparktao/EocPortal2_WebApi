using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Data.Repository.IRepository;
using Hexagon.Util.WebControl;

namespace Hexagon.Data.Repository.Repository
{
    public class RepositoryAsyn<T> : IRepositoryAsyn<T> where T : class, new()
    {
        #region 构造
        public IDatabaseAsync db;
        public RepositoryAsyn(IDatabaseAsync idatabase)
        {
            this.db = idatabase;
        }
        #endregion


        public async Task<List<T>> FindListBySql(string strSql)
        {
            return await db.FindListBySql<T>(strSql);
        }

        public async Task<T> FindEntityById(int id)
        {
            return await db.FindEntity<T>(id);
        }

    }
}
