using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Data.DataAccess
{
    public class DbHelperAsync
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DatabaseType DbType { get; set; }
        /// <summary>
        /// 数据库命名参数符号
        /// </summary>
        public static string DbParmChar { get; set; }
        public DbHelperAsync(string connstring)
        {
            ConnectionString = connstring;
            this.DatabaseTypeEnumParse("System.Data.OracleClient");
            DbParmChar = DbFactory.CreateDbParmCharacter();
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                using (DbConnection conn = DbFactory.CreateDbConnection(ConnectionString))
                {
                    await PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                    num = await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                using (DbConnection conn = DbFactory.CreateDbConnection(ConnectionString))
                {
                    await PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                    num = await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
                //log.Error(ex.Message);
            }
            return num;
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                await PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                num = await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                await PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, null);
                num = await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                if (cmdText.Contains("Base_Subsystem_Function"))
                {
                    //子系统功能点
                    using (DbConnection conn = DbFactory.CreateDbConnection(ConnectionString))
                    {
                        await PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, parameters);
                        num = await cmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    if (isOpenTrans == null || isOpenTrans.Connection == null)
                    {
                        using (DbConnection conn = DbFactory.CreateDbConnection(ConnectionString))
                        {
                            await PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, parameters);
                            num = await cmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        await PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                        num = await cmd.ExecuteNonQueryAsync();
                    }
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }


        /// <summary>
        /// 使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行<</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static async Task<DbDataReader> ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DbCommand cmd = DbFactory.CreateDbCommand();
            DbConnection conn = DbFactory.CreateDbConnection(ConnectionString);
            try
            {
                await PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                DbDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                throw;
            }
        }

        /// <summary>
        ///使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行<</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static async Task<DbDataReader> ExecuteReader(CommandType cmdType, string cmdText)
        {
            DbCommand cmd = DbFactory.CreateDbCommand();
            DbConnection conn = DbFactory.CreateDbConnection(ConnectionString);
            try
            {
                await PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                DbDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static async Task<object>  ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand();
                using (DbConnection connection = DbFactory.CreateDbConnection(ConnectionString))
                {
                    await PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                    object val = await cmd.ExecuteScalarAsync();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 为即将执行准备一个命令
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="isOpenTrans">DbTransaction对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        public static async Task PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }


        /// <summary>
        /// 用于数据库类型的字符串枚举转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public void DatabaseTypeEnumParse(string value)
        {
            try
            {
                switch (value)
                {
                    case "System.Data.SqlClient":
                        DbType = DatabaseType.SqlServer;
                        break;
                    case "System.Data.OracleClient":
                        DbType = DatabaseType.Oracle;
                        break;
                    case "MySql.Data.MySqlClient":
                        DbType = DatabaseType.MySql;
                        break;
                    case "System.Data.OleDb":
                        DbType = DatabaseType.Access;
                        break;
                    case "System.Data.SQLite":
                        DbType = DatabaseType.SQLite;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw new Exception("数据库类型\"" + value + "\"错误，请检查！");
            }
        }
    }
}
