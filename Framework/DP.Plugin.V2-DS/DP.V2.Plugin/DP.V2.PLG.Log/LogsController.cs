using DP.V2.PLG.Log.Actions;
using Microsoft.AspNetCore.Mvc;

namespace DP.V2.PLG.Log
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        [HttpPost]
        [Route("search")]
        public IActionResult SearchAllLog([FromBody]SearchAllLogAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }
    }
}
