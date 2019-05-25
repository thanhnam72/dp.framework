using System.Net;

namespace DP.V2.Core.Common.Base
{
    public class BaseServiceResult
    {
        public object Result { get; set; }
        public HttpStatusCode ReturnCode { get; set; }
        public bool Success { get; set; }
    }

    public class BaseServiceResult<T>
    {
        public BaseResponse<T> Result { get; set; }
        public HttpStatusCode ReturnCode { get; set; }
        public bool Success { get; set; }
    }
}
