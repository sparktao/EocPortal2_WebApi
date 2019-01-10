using Hexagon.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Data.Repository.IRepository
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 
    /// 创建人：
    /// 日 期：2015.10.10
    /// 描 述：定义仓储模型中的数据标准操作接口
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IRepositoryAsyn<T> where T : class, new()
    {

        Task<int> ExecuteBySql(string strSql);

        Task<int> ExecuteBySql(string strSql, DbParameter[] parameters);

        Task<List<T>> FindListBySql(string strSql);

        Task<PaginatedList<T>> FindListPageBySql(string strSql, Pagination pagination);

        Task<PaginatedList<T>> FindListPageBySql(string strSql, DbParameter[] parameters, Pagination pagination);

        Task<T> FindEntityById(object id);

        Task<int> Insert(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(object id);

    }
}
