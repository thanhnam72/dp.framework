using DP.V2.PLG.Identity.Actions;
using Microsoft.AspNetCore.Mvc;

namespace DP.V2.PLG.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpPost]
        [Route("all")]
        public IActionResult GetAll([FromBody]GetAllAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("get")]
        public IActionResult GetUser([FromBody]GetUserAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("croup")]
        public IActionResult CreateOrUpdateUser([FromBody]CreateOrUpdateUserAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult RemoveUser([FromBody]RemoveUserAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }
    }
}
