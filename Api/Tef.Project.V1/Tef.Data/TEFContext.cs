using DP.V2.Core.Common.Base;
using DP.V2.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Tef.Data
{
    public class TEFContext : DataContext
    {
        public TEFContext(DbContextOptions<TEFContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddGlobalFilter<BaseEntity>(this.GetType());
            modelBuilder.AddEntityCommon();
            base.OnModelCreating(modelBuilder);
        }
    }
}
