﻿using Saturno.Domain.Commands;
using Saturno.Domain.Commands.Contracts;
using Saturno.Domain.Entities;
using Saturno.Domain.Handlers.Contracts;
using Saturno.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Saturno.Domain.Handlers
{
    public class UserHandler : IHandler<RegisterUser>, IHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> Handle(RegisterUser command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Verifique mensagens", command.ValidationResult);

            var user = new User(Guid.NewGuid(), command.Name, command.Email, command.Password);

            await _userRepository.Register(user);

            return new GenericCommandResult(true, "Usuário salvo com sucesso!", user);
        }

        public async Task<ICommandResult> Handle(UpdateUser command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Verifique mensagens", command.ValidationResult);

            var user = await _userRepository.GetById(command.Id);

            if (user == null)
                return new GenericCommandResult(false, "Usuário não encontrado");

            user.UpdateName(command.Name);
            
            await _userRepository.Update(user);

            return new GenericCommandResult(true, "Usuário atualizado com sucesso!", user);
        }
    }
}