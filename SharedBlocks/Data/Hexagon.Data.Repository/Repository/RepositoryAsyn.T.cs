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

        public async Task<List<T>> FindListBySql(string strSql)
        {
            return await db.FindListBySql<T>(strSql);
        }

        public async Task<PaginatedList<T>> FindListPageBySql(string strSql, Pagination pagination)
        {
            string orderField = pagination.sidx;
            string orderType = pagination.sord;
            int pageIndex = pagination.PageIndex;
            int pageSize = pagination.PageSize;
            Tuple < int, List<T>> result = await db.FindListPageBySql<T>(strSql, orderField, orderType, pageIndex, pageSize);

            return new PaginatedList<T>(pageIndex, pageSize, result.Item1, result.Item2);
        }

        public async Task<PaginatedList<T>> FindListPageBySql(string strSql, DbParameter[] parameters, Pagination pagination)
        {
            string orderField = pagination.sidx;
            string orderType = pagination.sord;
            int pageIndex = pagination.PageIndex;
            int pageSize = pagination.PageSize;
            Tuple<int, List<T>> result = await db.FindListPageBySql<T>(strSql, parameters, orderField, orderType, pageIndex, pageSize);
            return new PaginatedList<T>(pageIndex, pageSize, result.Item1, result.Item2);
        }

        public async Task<T> FindEntityById(int id)
        {
            return await db.FindEntity<T>(id);
        }

        public async Task<T> FindEntityById(string id)
        {
            return await db.FindEntity<T>(id);
        }

        public async Task<int> Insert(T entity)
        {
            return await db.Insert<T>(entity);
        }

    }
}
