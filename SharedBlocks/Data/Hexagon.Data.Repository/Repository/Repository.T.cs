using Hexagon.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Hexagon.Data.Repository
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 
    /// 创建人：
    /// 日 期：
    /// 描 述：定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public class Repository<T> : IRepository<T> where T : class,new()
    {
        #region 构造
        public IDatabase db;
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 事物提交
        public IRepository<T> BeginTrans()
        {
            db.BeginTrans();
            return this;
        }
        public void Commit()
        {
            db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            return db.ExecuteBySql(strSql);
        }
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return db.ExecuteBySql(strSql, dbParameter);
        }
        public int ExecuteByProc(string procName)
        {
            return db.ExecuteByProc(procName);
        }
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            return db.ExecuteByProc(procName, dbParameter);
        }
        #endregion

        public IEnumerable<T> FindList(string strSql)
        {
            return db.FindListBySql<T>(strSql);
        }

        #region 数据源 查询
        public DataTable FindTable(string strSql)
        {
            return db.FindTable<T>(strSql);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            return db.FindTable<T>(strSql, dbParameter);
        }
        public DataTable FindTable(string strSql, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindTablePage<T>(strSql, pagination.sidx, pagination.sord,  pagination.rows, pagination.page, ref total);
            pagination.records = total;
            return data;
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindTablePage<T>(strSql, dbParameter, pagination.sidx, pagination.sord, pagination.rows, pagination.page, ref total);
            pagination.records = total;
            return data;
        }

        #endregion
    }
}
