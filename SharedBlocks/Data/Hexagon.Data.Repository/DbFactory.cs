using Hexagon.Data.DataAccess;
using System;

namespace Hexagon.Data.Repository
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2018-2020
    /// 创建人：
    /// 日 期：2018.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DatabaseType DbType)
        {
            DbHelper.DbType = DbType;
            return new Database(connString);
        }

        public static IDatabaseAsync BaseAsyn(string connString, DatabaseType DbType)
        {
            DbHelper.DbType = DbType;
            return new DatabaseAsync(connString);
        }

        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            //DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), UnityIocHelper.GetmapToByName("DBcontainer", "IDbContext"));
            return new Database("");
        }
    }
}
