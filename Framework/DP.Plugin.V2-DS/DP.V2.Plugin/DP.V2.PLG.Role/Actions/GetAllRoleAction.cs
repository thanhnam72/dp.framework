using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Role.Businesses;
using DP.V2.PLG.Role.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Role.Actions
{
    public class GetAllRoleAction : BaseActionCommand<IRoleBusiness>
    {
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<GetAllRoleResponse>();
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
