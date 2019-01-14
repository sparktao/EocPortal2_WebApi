using Hexagon.Data.DbExpand;
using Hexagon.Data.EF.Extension;
using Hexagon.Data.Entity;
using Hexagon.Data.Extension;
using Hexagon.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagon.Data.EF
{
    public class DatabaseAsync : IDatabaseAsync
    {
        public DatabaseAsync(DbContext pContext) {
            this.dbcontext = pContext;
        }
        
        #region 属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public DbContext dbcontext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbContextTransaction dbTransaction { get; set; }
        #endregion

        #region 事物提交
        ///// <summary>
        ///// 事务开始
        ///// </summary>
        ///// <returns></returns>
        public IDatabaseAsync BeginTrans()
        {
            //DbConnection dbConnection = ((IObjectContextAdapter)dbcontext).ObjectContext.Connection;
            //if (dbConnection.State == ConnectionState.Closed)
            //{
            //    dbConnection.Open();
            //}
            dbTransaction = dbcontext.Database.BeginTransaction();
            return this;
        }
        ///// <summary>
        ///// 提交当前操作的结果
        ///// </summary>
        public async Task<int> Commit()
        {
            try
            {
                int returnValue = await dbcontext.SaveChangesAsync();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    this.Close();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    string msg = ExceptionMessage.GetSqlExceptionMessage(sqlEx.Number);
                    throw DataAccessException.ThrowDataAccessException(sqlEx, msg);
                }
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        ///// <summary>
        ///// 把当前操作回滚成未提交状态
        ///// </summary>
        public void Rollback()
        {
            this.dbTransaction.Rollback();
            this.dbTransaction.Dispose();
            this.Close();
        }
        ///// <summary>
        ///// 关闭连接 内存回收
        ///// </summary>
        public void Close()
        {
            dbcontext.Dispose();
        }
        #endregion

        #region 执行SQL语句
        public async Task<int> ExecuteBySql(string strSql)
        {
            if (dbTransaction == null)
            {
                return await dbcontext.Database.ExecuteSqlCommandAsync(strSql);
            }
            else
            {
                await dbcontext.Database.ExecuteSqlCommandAsync(strSql);
                return dbTransaction == null ? await this.Commit() : 0;
            }
        }

        public async Task<int> ExecuteBySql(string strSql, DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                return await dbcontext.Database.ExecuteSqlCommandAsync(strSql, dbParameter);
            }
            else
            {
                await dbcontext.Database.ExecuteSqlCommandAsync(strSql, dbParameter);
                return dbTransaction == null ? await this.Commit() : 0;
            }
        }

        #endregion

        #region 执行存储过程

        public async Task<int> ExecuteByProc(string procName)
        {
            if (dbTransaction == null)
            {
                return await dbcontext.Database.ExecuteSqlCommandAsync(DbContextExtensions.BuilderProc(procName));
            }
            else
            {
                await dbcontext.Database.ExecuteSqlCommandAsync(DbContextExtensions.BuilderProc(procName));
                return dbTransaction == null ? await this.Commit() : 0;
            }
        }


        public async Task<int> ExecuteByProc(string procName, DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                return await dbcontext.Database.ExecuteSqlCommandAsync(DbContextExtensions.BuilderProc(procName), dbParameter);
            }
            else
            {
                await dbcontext.Database.ExecuteSqlCommandAsync(DbContextExtensions.BuilderProc(procName), dbParameter);
                return dbTransaction == null ? await this.Commit() : 0;
            }
        }
        #endregion

        #region 插入数据

        public async Task<int> Insert<T>(T entity) where T : class
        {
            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbTransaction == null ? await this.Commit() : 0;
        }

        public async Task<int> Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? await this.Commit() : 0;
        }
        #endregion

        #region 修改数据
        public async Task<int> Update<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            Hashtable props = ConvertExtension.GetPropertyInfo<T>(entity);
            //获取主键属性名称
            var primarykeyName = dbcontext.Model.FindEntityType(typeof(T)).FindPrimaryKey()
                .Properties.Select(x => x.Name).Single();

            foreach (string item in props.Keys)
            {
                object value = dbcontext.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    if (value.ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(item).CurrentValue = null;

                    if (!item.ToUpper().Equals(primarykeyName.ToUpper()))
                    {
                        //主键不能设置IsModified
                        dbcontext.Entry(entity).Property(item).IsModified = true;
                    }
                }
            }
            return dbTransaction == null ? await this.Commit() : 0;
        }

        public async Task<int> Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                await this.Update(entity);
            }
            return dbTransaction == null ? await this.Commit() : 0;
        }

        public async Task<int> Update<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return 0;
        }

        #endregion

        #region 删除数据
        public async Task<int> Delete<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Set<T>().Remove(entity);
            return dbTransaction == null ? await this.Commit() : 0;
        }

        public async Task<int> Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Set<T>().Attach(entity);
                dbcontext.Set<T>().Remove(entity);
            }
            return dbTransaction == null ? await this.Commit() : 0;
        }

        public Task<int> Delete<T>(object keyValue)
        {
            //EntitySet entitySet = DbContextExtensions.GetEntitySet<T>(dbcontext);
            //if (entitySet != null)
            //{
            //    string tableName = entitySet.MetadataProperties.Contains("Table") && entitySet.MetadataProperties["Table"].Value != null
            //                   ? entitySet.MetadataProperties["Table"].Value.ToString()
            //                   : entitySet.Name;
            //    string keyFlied = entitySet.ElementType.KeyMembers[0].Name;
            //    return this.ExecuteBySql(DbContextExtensions.DeleteSql(tableName, keyFlied, keyValue));
            //}
            //return -1;

            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(object propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string propertyName, string propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(string tableName, string propertyName, string propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(object[] propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(object[] propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string propertyName, object[] propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(string tableName, string propertyName, object[] propertyValue)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 查询数据列表、返回List
        public async Task<IEnumerable<T>> FindList<T>() where T : class, new()
        {
            return await dbcontext.Set<T>().ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql) where T : class
        {
            return await FindList<T>(strSql, null);
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] parameters) where T : class
        {            
            using (var dbConnection = dbcontext.Database.GetDbConnection())
            {
                var IDataReader = await new DbHelperAsync(dbConnection).ExecuteReader(CommandType.Text, strSql, parameters);
                return ConvertExtension.IDataReaderToList<T>(IDataReader);
            }
        }
        
        public async Task<Tuple<int, List<T>>> FindList<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize)
        {
            return await FindList<T>(strSql, null, orderField, orderType, pageIndex, pageSize);
        }

        public async Task<Tuple<int, List<T>>> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, string orderType, int pageIndex, int pageSize)
        {
            using (DbConnection dbConnection = dbcontext.Database.GetDbConnection())
            {
                if (DbHelperAsync.DbType == DatabaseType.SQLite)
                {
                    return await new SqliteHelper().GetPageListAsync<T>(dbConnection, strSql, dbParameter, orderField, orderType, pageIndex, pageSize);
                }
                else
                {
                    return await SqlServerHelper.GetPageListAsync<T>(dbConnection, strSql, dbParameter, orderField, orderType, pageIndex, pageSize);
                }
            }
        }

        #endregion

        public Task<DataTable> FindTable<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable<T>(string WhereSql) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTableByProc(string procName)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTableByProc(string procName, DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTableBySql(string strSql)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTableBySql(string strSql, DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, DataTable>> FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, DataTable>> FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, DataTable>> FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, DataTable>> FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, DataTable>> FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }



        #region 查询对象、返回实体
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public async Task<T> FindEntity<T>(object keyValue) where T : class
        {
            return await dbcontext.Set<T>().FindAsync(keyValue);
        }

        public async Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            //return dbcontext.Set<T>().Where(condition).FirstOrDefault();
            throw new NotImplementedException();
        }
        #endregion
    }
}
