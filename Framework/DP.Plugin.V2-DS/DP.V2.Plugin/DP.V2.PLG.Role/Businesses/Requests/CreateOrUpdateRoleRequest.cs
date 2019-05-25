using DP.V2.Core.Common.Base;
using DP.V2.PLG.Role.Businesses.Dtos;

namespace DP.V2.PLG.Role.Businesses.Requests
{
    public class CreateOrUpdateRoleRequest : BaseRequest
    {
        public RoleDto Data { get; set; }
    }
}
