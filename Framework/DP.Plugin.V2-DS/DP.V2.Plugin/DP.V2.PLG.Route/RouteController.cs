using DP.V2.Core.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DP.V2.PLG.Route
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("gate")]
        public IActionResult Gate(string status)
        {
            return Ok("Server is " + status);
        }
    }
}
