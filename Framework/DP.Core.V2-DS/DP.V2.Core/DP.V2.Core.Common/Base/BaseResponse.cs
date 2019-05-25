namespace DP.V2.Core.Common.Base
{
    public class BaseResponse
    {
        public int ErrorCode { get; set; }
        public string Errors { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
