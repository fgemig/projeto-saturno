using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saturno.API.Services;
using Saturno.Domain.Commands;
using Saturno.Domain.Entities;
using Saturno.Domain.Handlers;
using System.Threading.Tasks;

namespace Saturno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromServices] UserHandler handler, [FromBody] LoginUser command)
        {
            if (command == null)
                return BadRequest("Usuário não informado");

            var result = await handler.Handle(command);

            if (result.Success)
            {
                result.Data = TokenService.GenerateToken((User)result.Data);
            }

            return Response(result);
        }
    }
}
