using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Role.Businesses;
using DP.V2.PLG.Role.Businesses.Requests;
using DP.V2.PLG.Role.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Role.Actions
{
    public class GetRoleAction : BaseActionCommand<IRoleBusiness>
    {
        public GetRoleRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<GetRoleResponse>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
