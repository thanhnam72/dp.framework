using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Role.Businesses;
using DP.V2.PLG.Role.Businesses.Requests;
using DP.V2.PLG.Role.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Role.Actions
{
    public class CreateOrUpdateRoleAction : BaseActionCommand<IRoleBusiness>
    {
        public CreateOrUpdateRoleRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<CreateOrUpdateRoleResponse>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
