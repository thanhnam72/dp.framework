using DP.V2.Core.Common.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Data.DataModel
{
    public class SysRole : BaseEntity
    {
        public new Guid Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Descriptions { get; set; }
    }
}
