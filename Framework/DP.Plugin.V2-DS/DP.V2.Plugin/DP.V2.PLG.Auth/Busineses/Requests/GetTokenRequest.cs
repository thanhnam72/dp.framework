using DP.V2.Core.Common.Base;

namespace DP.V2.PLG.Auth.Busineses.Requests
{
    public class GetTokenRequest : BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
