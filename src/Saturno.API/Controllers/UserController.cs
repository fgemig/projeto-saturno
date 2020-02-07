﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saturno.Domain.Commands;
using Saturno.Domain.Entities;
using Saturno.Domain.Handlers;
using Saturno.Domain.Repositories;
using System.Collections;
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

            var result = (GenericCommandResult)await handler.Handle(command);

            return Response(result);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IReadOnlyCollection<User>> List([FromServices]IUserRepository repository)
        {
            return await repository.GetAll();
        }
    }
}