using DP.V2.Core.Common.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Data.DataModel
{
    public class SysPermission : BaseEntity
    {
        [StringLength(10)]
        public string FnCd { get; set; }
        public Guid RoleId { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Remove { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }
        public string Other { get; set; }   
    }
}
