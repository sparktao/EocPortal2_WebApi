using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Data
{
    /// <summary>
    /// 异步操作数据库接口
    /// </summary>
    public interface IDatabaseAsync : IDisposable
    {
        Task<int> ExecuteBySql(string strSql);
        Task<int> ExecuteBySql(string strSql, DbTransaction isOpenTrans);
        Task<int> ExecuteBySql(string strSql, DbParameter[] parameters);
        Task<int> ExecuteBySql(string strSql, DbParameter[] parameters, DbTransaction isOpenTrans);

        #region 执行存储过程
        Task<int> ExecuteByProc(string procName);
        Task<int> ExecuteByProc(string procName, DbTransaction isOpenTrans);
        Task<int> ExecuteByProc(string procName, DbParameter[] parameters);
        Task<int> ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans);
        #endregion

        #region 添加数据
        Task<int> Insert<T>(T entity);
        Task<int> Insert<T>(T entity, DbTransaction isOpenTrans);
        Task<int> Insert<T>(List<T> entity);
        Task<int> Insert<T>(List<T> entity, DbTransaction isOpenTrans);
        #endregion

        #region 修改数据
        Task<int> Update<T>(T entity);
        Task<int> Update<T>(T entity, DbTransaction isOpenTrans);
        Task<int> Update<T>(string propertyName, string propertyValue);
        Task<int> Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        Task<int> Update<T>(List<T> entity);
        Task<int> Update<T>(List<T> entity, DbTransaction isOpenTrans);
        #endregion

        #region 删除数据
        Task<int> Delete<T>(T entity);
        Task<int> Delete<T>(T entity, DbTransaction isOpenTrans);
        Task<int> Delete<T>(object propertyValue);
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
        Task<List<T>> FindListTop<T>(int Top) where T : new();
        Task<List<T>> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new();
        Task<List<T>> FindListTop<T>(int Top, string WhereSql) where T : new();
        Task<List<T>> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
        Task<List<T>> FindList<T>() where T : new();
        Task<List<T>> FindList<T>(string propertyName, string propertyValue) where T : new();
        Task<List<T>> FindList<T>(string WhereSql) where T : new();
        Task<List<T>> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new();
        Task<List<T>> FindListBySql<T>(string strSql);
        Task<List<T>> FindListBySql<T>(string strSql, DbParameter[] parameters);
        Task<Tuple<int, List<T>>> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, List<T>>> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, List<T>>> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize) where T : new();
        Task<Tuple<int, List<T>>> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize);
        Task<Tuple<int, List<T>>> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize);
        Task<Tuple<int, List<T>>> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderby, int pageIndex, int pageSize);
        #endregion

        #region 查询数据列表、DataTable

        Task<DataTable> FindTableTop<T>(int Top) where T : new();
        Task<DataTable> FindTableTop<T>(int Top, string WhereSql) where T : new();
        Task<DataTable> FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
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


        #region

        Task<T> FindEntity<T>(object propertyValue) where T : new();
        Task<T> FindEntity<T>(string propertyName, object propertyValue) where T : new();
        Task<T> FindEntityByWhere<T>(string WhereSql) where T : new();
        Task<T> FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new();
        Task<T> FindEntityBySql<T>(string strSql);
        Task<T> FindEntityBySql<T>(string strSql, DbParameter[] parameters);

        #endregion


    }
}
