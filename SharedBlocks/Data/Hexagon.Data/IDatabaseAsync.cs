using Hexagon.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Data
{
    /// <summary>
    /// 异步操作数据库接口
    /// </summary>
    public interface IDatabaseAsync
    {
        #region 执行SQL语句
        Task<int> ExecuteBySql(string strSql);        
        Task<int> ExecuteBySql(string strSql, DbParameter[] dbParameter);
        #endregion

        #region 执行存储过程
        Task<int> ExecuteByProc(string procName);
        Task<int> ExecuteByProc(string procName, DbParameter[] dbParameter);
        #endregion

        #region 添加数据
        Task<int> Insert<T>(T entity) where T : class;
        Task<int> Insert<T>(IEnumerable<T> entities) where T : class;
        #endregion

        #region 修改数据
        Task<int> Update<T>(T entity) where T : class;
        Task<int> Update<T>(IEnumerable<T> entities) where T : class;

        Task<int> Update<T>(Expression<Func<T, bool>> condition) where T : class, new();
        #endregion

        #region 删除数据

        Task<int> Delete<T>(T entity) where T : class;

        Task<int> Delete<T>(IEnumerable<T> entities) where T : class;
        Task<int> Delete<T>(object keyValue);
        Task<int> Delete<T>(object propertyValue, DbTransaction isOpenTrans);
        Task<int> Delete<T>(string propertyName, string propertyValue);
        Task<int> Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        Task<int> Delete(string tableName, string propertyName, string propertyValue);
        Task<int> Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans);
        Task<int> Delete<T>(object[] propertyValue);
        Task<int> Delete<T>(object[] propertyValue, DbTransaction isOpenTrans);
        Task<int> Delete<T>(string propertyName, object[] propertyValue);
        Task<int> Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans);
        Task<int> Delete(string tableName, string propertyName, object[] propertyValue);
        Task<int> Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans);
        #endregion

        #region 查询数据列表、返回List

        Task<IEnumerable<T>> FindList<T>() where T : class, new();
        Task<IEnumerable<T>> FindList<T>(string strSql) where T : class;
        Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] parameters) where T : class;
        Task<Tuple<int, List<T>>> FindList<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize);
        Task<Tuple<int, List<T>>> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, string orderType, int pageIndex, int pageSize);
        #endregion

        #region 查询数据列表、DataTable

        Task<DataTable> FindTable<T>() where T : new();
        Task<DataTable> FindTable<T>(string WhereSql) where T : new();
        Task<DataTable> FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new();
        Task<DataTable> FindTableBySql(string strSql);
        Task<DataTable> FindTableBySql(string strSql, DbParameter[] parameters);
        Task<Tuple<int, DataTable>> FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, DataTable>> FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, DataTable>> FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, DataTable>> FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize);
        Task<Tuple<int, DataTable>> FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize);
        Task<DataTable> FindTableByProc(string procName);
        Task<DataTable> FindTableByProc(string procName, DbParameter[] parameters);

        #endregion

        #region 查询实体

        Task<T> FindEntity<T>(object KeyValue) where T : class;
        Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new();

        #endregion

    }
}
