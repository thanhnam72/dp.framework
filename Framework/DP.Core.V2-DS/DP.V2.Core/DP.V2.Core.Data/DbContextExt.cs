using DP.V2.Core.Common.Base;
using DP.V2.Core.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace DP.V2.Core.Data
{
    public static class DbContextExtensions
    {
        public static void AddGlobalFilter<T>(this ModelBuilder modelBuilder,Type dbContext) where T : BaseEntity
        {
            var allTypeThathasDeleted = dbContext.Assembly.GetExportedTypes()
                           .Where(x=>x.IsSubclassOf(typeof(BaseEntity))).ToList();
            
            foreach (var type in allTypeThathasDeleted)
            {
                var makeGenericMethod = typeof(DbContextExtensions)
                  .GetMethod("AddGlobalFilterToEntity", BindingFlags.Static | BindingFlags.NonPublic)
                  .MakeGenericMethod(type);
                makeGenericMethod.Invoke(null, new[] { modelBuilder });
            }

        }

        public static void AddEntityCommon(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysUser>().ToTable("SysUser").HasKey(x => x.Id);
            modelBuilder.Entity<SysRole>().ToTable("SysRole").HasKey(x => x.Id);
            modelBuilder.Entity<SysPermission>().ToTable("SysPermission").HasKey(x => x.Id);
            modelBuilder.Entity<SysFunction>().ToTable("SysFunction").HasKey(x => x.Id);
            modelBuilder.Entity<SysLog>().ToTable("SysLog").HasKey(x => x.Id);
        }

        private  static void AddGlobalFilterToEntity<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(p => !p.Deleted);
        }
    }
}
