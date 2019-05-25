using DP.V2.Core.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Data.DataModel
{
    public class SysLog : BaseEntity
    {
        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string ActionName { get; set; }

        public string Descriptions { get; set; }
    }
}
