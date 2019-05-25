using DP.V2.Core.Common.Base;
using System;
using System.Collections.Generic;

namespace DP.V2.PLG.Role.Businesses.Requests
{
    public class RemoveRoleRequest : BaseRequest
    {
        public IList<Guid?> Ids { get; set; }
    }
}
