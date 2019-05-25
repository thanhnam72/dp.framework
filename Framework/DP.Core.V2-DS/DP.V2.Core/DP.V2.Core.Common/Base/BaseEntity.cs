using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DP.V2.Core.Common.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        [DefaultValue("now()")]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(100)]
        public string CreateBy { get; set; }

        [StringLength(100)]
        public string UpdateBy { get; set; }

        public bool Deleted { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseEntity()
        {
            IsActive = true;
            Deleted = false;
            CreateBy = string.Empty;
            CreatedDate = DateTime.Now;
        }
    }
}
