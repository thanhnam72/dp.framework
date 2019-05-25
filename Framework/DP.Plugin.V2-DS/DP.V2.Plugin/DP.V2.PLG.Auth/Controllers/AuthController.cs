using DP.V2.Core.WebApi.Attributes;
using DP.V2.PLG.Auth.Actions;
using Microsoft.AspNetCore.Mvc;

namespace DP.V2.PLG.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody]GetTokenAction action)
        {
            var result = action.ExecuteAsync();
            return Ok(result.Result);
        }
    }
}
