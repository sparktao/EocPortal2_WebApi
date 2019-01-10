using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Data.DataAccess.DbExpand
{
    public class OracleHelper
    {
        public static async Task<Tuple<int, DataTable>> GetPageTableAsync(string sql, DbParameter[] param, string orderField, string orderType, int pageIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            string OrderBy = "";
            if (!string.IsNullOrEmpty(orderField))
                OrderBy = "Order By " + orderField + " " + orderType + "";
            else
                OrderBy = "order by (select 0)";
            strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            strSql.Append("  rowNumber, T.* From (" + sql + ")  T )  N Where rowNumber > " + num + " And rowNumber <= " + num1 + "");
            int count = Convert.ToInt32(await DbHelperAsync.ExecuteScalar(CommandType.Text, "Select Count(1) From (" + sql + ")  t", param));
            DbDataReader dr = await DbHelperAsync.ExecuteReader(CommandType.Text, strSql.ToString(), param);
            DataTable dt = await DatabaseReader.ReaderToDataTableAsync(dr);
            return new Tuple<int, DataTable>(count, dt);
        }

        public static async Task<Tuple<int, List<T>>> GetPageListAsync<T>(string sql, DbParameter[] param, string orderField, string orderType, int pageIndex, int pageSize)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                int num = (pageIndex - 1) * pageSize;
                int num1 = (pageIndex) * pageSize;
                string OrderBy = "";
                if (!string.IsNullOrEmpty(orderField))
                    OrderBy = "Order By " + orderField + " " + orderType + "";
                else
                    OrderBy = "Order By (select 0)";
                strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
                strSql.Append("  rowNumber, T.* From (" + sql + ")  T )  N Where rowNumber > " + num + " And rowNumber <= " + num1 + "");
                int count = Convert.ToInt32(await DbHelperAsync.ExecuteScalar(CommandType.Text, "Select Count(1) From (" + sql + ") t", param));
                DbDataReader dr = await DbHelperAsync.ExecuteReader(CommandType.Text, strSql.ToString(), param);
                List<T> resultlst = await DatabaseReader.ReaderToList<T>(dr);
                return new Tuple<int, List<T>>(count, resultlst);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
