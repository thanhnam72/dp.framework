using DP.V2.Core.Common.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Data.DataModel
{
    public class SysUser : BaseEntity
    {
        [StringLength(100)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Fullname { get; set; }
        public Guid? RoleId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExp { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
