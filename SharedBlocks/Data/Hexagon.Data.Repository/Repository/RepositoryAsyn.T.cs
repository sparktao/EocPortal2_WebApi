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

        public async Task<int> ExecuteBySql(string strSql)
        {
            return await db.ExecuteBySql(strSql);
        }

        public async Task<int> ExecuteBySql(string strSql, DbParameter[] parameters)
        {
            return await db.ExecuteBySql(strSql, parameters);
        }

        public async Task<IEnumerable<T>> FindList(string strSql)
        {
            return await db.FindList<T>(strSql);
        }

        public async Task<PaginatedList<T>> FindListPageBySql(string strSql, Pagination pagination)
        {
            string orderField = pagination.sidx;
            string orderType = pagination.sord;
            int pageIndex = pagination.PageIndex;
            int pageSize = pagination.PageSize;
            Tuple < int, List<T>> result = await db.FindList<T>(strSql, orderField, orderType, pageIndex, pageSize);

            return new PaginatedList<T>(pageIndex, pageSize, result.Item1, result.Item2);
        }

        public async Task<PaginatedList<T>> FindListPageBySql(string strSql, DbParameter[] parameters, Pagination pagination)
        {
            string orderField = pagination.sidx;
            string orderType = pagination.sord;
            int pageIndex = pagination.PageIndex;
            int pageSize = pagination.PageSize;
            Tuple<int, List<T>> result = await db.FindList<T>(strSql, parameters, orderField, orderType, pageIndex, pageSize);
            return new PaginatedList<T>(pageIndex, pageSize, result.Item1, result.Item2);
        }

        public async Task<T> FindEntityById(object id)
        {
            return await db.FindEntity<T>(id);
        }

        public async Task<int> Insert(T entity)
        {
            return await db.Insert<T>(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await db.Update<T>(entity);
        }

        public async Task<int> Delete(object id)
        {
            return await db.Delete<T>(id);
        }

    }
}
