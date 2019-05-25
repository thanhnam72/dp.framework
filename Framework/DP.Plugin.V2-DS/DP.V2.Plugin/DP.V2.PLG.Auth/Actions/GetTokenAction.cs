using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Auth.Busineses;
using DP.V2.PLG.Auth.Busineses.Dtos;
using DP.V2.PLG.Auth.Busineses.Requests;
using System.Threading.Tasks;

namespace DP.V2.PLG.Auth.Actions
{
    public class GetTokenAction : BaseActionCommand<IAuthBusiness>
    {
        public GetTokenRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<BaseResponse<TokenDto>>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
