using System;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hexagon.Data.Entity;
using Hexagon.Data.Entity.EntityConfiguration;

namespace Hexagon.Data.EF.Context
{
    /// <summary>
    /// 描 述：数据访问(SqlServer) 上下文
    /// </summary>
    public class SqliteDbContext : DbContext
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        /// <param name="connString"></param>
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=eoc.db");
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Organization_EmployeeConfiguration());
        }


        public DbSet<Organization_Employee> Organization_Employee { get; set; }
    }
}
