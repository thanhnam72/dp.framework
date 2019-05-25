using DP.V2.Core.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Data.DataModel
{
    public class SysFunction : BaseEntity
    {
        [StringLength(10)]
        public string FnCd { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string ActionName { get; set; }
    }
}
