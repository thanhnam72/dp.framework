using DP.V2.Core.Common.Mapper;
using DP.V2.Core.Data.DataModel;
using DP.V2.Core.Data.Interface;
using DP.V2.PLG.Log.Businesses.Dtos;
using DP.V2.PLG.Log.Businesses.Requests;
using DP.V2.PLG.Log.Businesses.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace DP.V2.PLG.Log.Businesses
{
    public class LogBusiness : ILogBusiness
    {
        private readonly IRepository<SysLog> _repoLog;
        public LogBusiness(IRepository<SysLog> repoLog)
        {
            _repoLog = repoLog;
        }

        public Task<SearchAllLogResponse> SearchAllLog(SearchAllLogRequest request)
        {
            var item = _repoLog.GetAll().AsEnumerable();

            var results = AutoMapper.MapList<SysLog, LogDto>(item);

            return Task.FromResult(new SearchAllLogResponse
            {
                Data = results
            });
        }
    }
}
