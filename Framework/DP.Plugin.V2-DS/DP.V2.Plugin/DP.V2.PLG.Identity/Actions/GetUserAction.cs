using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Identity.Businesses;
using DP.V2.PLG.Identity.Businesses.Requests;
using DP.V2.PLG.Identity.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Identity.Actions
{
    public class GetUserAction : BaseActionCommand<IIdentityBusiness>
    {
        public GetUserRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<GetUserResponse>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
