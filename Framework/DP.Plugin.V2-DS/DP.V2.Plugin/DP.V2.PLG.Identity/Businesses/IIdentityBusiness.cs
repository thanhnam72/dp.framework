using DP.V2.Core.Common.Base;
using DP.V2.PLG.Identity.Businesses.Requests;
using DP.V2.PLG.Identity.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Identity.Businesses
{
    public interface IIdentityBusiness
    {
        Task<GetUserResponse> GetUser(GetUserRequest request);
        Task<BaseResponse<string>> CreateOrUpdateUser(CreateOrUpdateUserRequest request);
        Task<BaseResponse<bool>> RemoveUser(RemoveUserRequest request);
        Task<GetAllResponse> GetAll();
    }
}
