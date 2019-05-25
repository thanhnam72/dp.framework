using DP.V2.Core.Common.Base;
using System;

namespace DP.V2.PLG.Identity.Businesses.Requests
{
    public class GetUserRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
