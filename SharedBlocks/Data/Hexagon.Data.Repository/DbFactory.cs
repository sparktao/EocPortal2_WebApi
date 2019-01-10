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
        public static IDatabaseAsync BaseAsyn(string connString, DatabaseType DbType)
        {
            DbHelperAsync.DbType = DbType;
            return new DatabaseAsync(connString);
        }


    }
}
