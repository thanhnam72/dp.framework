using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Action;
using DP.V2.PLG.Log.Businesses;
using DP.V2.PLG.Log.Businesses.Requests;
using DP.V2.PLG.Log.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Log.Actions
{
    public class SearchAllLogAction : BaseActionCommand<ILogBusiness>
    {
        public SearchAllLogRequest RequestData { get; set; }
        public override Task<BaseServiceResult> ExecuteAsync()
        {
            return Execute<SearchAllLogResponse>(RequestData);
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
