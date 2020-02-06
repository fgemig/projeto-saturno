using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Saturno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        [Route("")]
        public IActionResult Index() {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("admin")]
        public IActionResult Admin()
        {
            return Ok();
        }
    }
}
