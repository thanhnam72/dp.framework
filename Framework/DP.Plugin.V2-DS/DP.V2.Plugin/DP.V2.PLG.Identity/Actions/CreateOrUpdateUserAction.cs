using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Identity.Businesses;
using DP.V2.PLG.Identity.Businesses.Requests;
using System.Threading.Tasks;

namespace DP.V2.PLG.Identity.Actions
{
    public class CreateOrUpdateUserAction : BaseActionCommand<IIdentityBusiness>
    {
        public CreateOrUpdateUserRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<BaseResponse<string>>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
