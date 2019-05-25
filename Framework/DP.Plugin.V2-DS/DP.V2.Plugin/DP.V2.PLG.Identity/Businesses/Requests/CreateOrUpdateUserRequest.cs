using DP.V2.Core.Common.Base;
using DP.V2.PLG.Identity.Businesses.Dtos;

namespace DP.V2.PLG.Identity.Businesses.Requests
{
    public class CreateOrUpdateUserRequest : BaseRequest
    {
        public UserDto Data { get; set; }
    }
}
