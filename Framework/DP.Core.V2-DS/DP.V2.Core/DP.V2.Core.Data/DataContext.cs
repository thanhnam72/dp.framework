using DP.V2.Core.Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DP.V2.Core.Data
{
    /// <summary>
    /// Data context
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Data context constructor
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions options) : base(options)
        {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysFunction>().ToTable("SysFunction").HasKey(x => x.Id);
            modelBuilder.Entity<SysLog>().ToTable("SysLog").HasKey(x => x.Id);
            modelBuilder.Entity<SysRole>().ToTable("SysRole").HasKey(x => x.Id);
            modelBuilder.Entity<SysUser>().ToTable("SysUser").HasKey(x => x.Id);
            modelBuilder.Entity<SysPermission>().ToTable("SysPermission").HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
