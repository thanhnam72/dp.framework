using DP.V2.Core.Common.Base;
using System;

namespace DP.V2.PLG.Role.Businesses.Requests
{
    public class GetRoleRequest : BaseRequest
    {
        public Guid? Id { get; set; }
    }
}
