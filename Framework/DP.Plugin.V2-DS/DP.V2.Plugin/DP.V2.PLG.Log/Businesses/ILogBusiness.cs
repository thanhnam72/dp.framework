using DP.V2.PLG.Log.Businesses.Requests;
using DP.V2.PLG.Log.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Log.Businesses
{
    public interface ILogBusiness
    {
        Task<SearchAllLogResponse> SearchAllLog(SearchAllLogRequest request);
    }
}
