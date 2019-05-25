using DP.V2.Core.Common.Base;
using DP.V2.Core.Data;
using DP.V2.Core.Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DP.V2.PLG.Sample.Model
{
    public class PGContext : DataContext
    {
        public PGContext(DbContextOptions<PGContext> options) : base(options)
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
