using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saturno.Domain.Commands;
using Saturno.Domain.Commands.Handlers;
using Saturno.Domain.Entities;
using Saturno.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saturno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromServices] UserHandler handler, [FromBody] RegisterUser command)
        {
            if (command == null)
                return BadRequest("Dados do usuário não informados");            

            var result = await handler.Handle(command);

            return Response(result);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public IReadOnlyCollection<User> List([FromServices]IUserRepository repository)
        {
            return repository.GetAll();
        }
    }
}
