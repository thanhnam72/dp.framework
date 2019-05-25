using DP.V2.PLG.Role.Actions;
using Microsoft.AspNetCore.Mvc;

namespace DP.V2.PLG.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpPost]
        [Route("all")]
        public IActionResult GetAllRole([FromBody]GetAllRoleAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("get")]
        public IActionResult GetRole([FromBody]GetRoleAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("croup")]
        public IActionResult CreateOrUpdateRole([FromBody]CreateOrUpdateRoleAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult RemoveRole([FromBody]RemoveRoleAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }

        [HttpPost]
        [Route("functions/getbyrole")]
        public IActionResult GetFunctionByRole([FromBody]GetFunctionByRoleAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }
    }
}
