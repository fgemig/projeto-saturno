using Microsoft.AspNetCore.Mvc;
using Saturno.Domain.Models;

namespace Saturno.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Response(GenericCommandResult commandResult)
        {
            var obj = new
            {
                commandResult.Success,
                commandResult.Message,
                commandResult.Data
            };

            if (commandResult.Success)
                return Ok(obj);

            return BadRequest(obj);
        }
    }
}
