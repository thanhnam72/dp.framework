using DP.V2.Core.Common.Base;
using DP.V2.PLG.Auth.Busineses.Dtos;
using DP.V2.PLG.Auth.Busineses.Requests;
using System.Threading.Tasks;

namespace DP.V2.PLG.Auth.Busineses
{
    public interface IAuthBusiness
    {
        Task<BaseResponse<TokenDto>> GetToken(GetTokenRequest request);
    }
}
