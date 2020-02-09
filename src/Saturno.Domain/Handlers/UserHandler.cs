using Saturno.Domain.Commands;
using Saturno.Domain.Contracts;
using Saturno.Domain.Entities;
using Saturno.Domain.Enums;
using Saturno.Domain.Events;
using Saturno.Domain.Helpers;
using Saturno.Domain.Models;
using Saturno.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Saturno.Domain.Handlers
{
    public class UserHandler : IHandler<RegisterUser>, IHandler<UpdateUser>, IHandler<LoginUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;
        private readonly IUnitOfWork _unitOfWork;
        public UserHandler(IUserRepository userRepository, IEventBus eventBus, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
            _unitOfWork = unitOfWork;
        }

        public Task<GenericCommandResult> Handle(RegisterUser command)
        {
            command.Validate();

            if (command.Invalid)
                return Task.FromResult(new GenericCommandResult(false, "Verifique mensagens", command.ValidationResult));

            var user = new User(Guid.NewGuid(), command.Name, command.Email, command.Password);

            user.SetUserRole(UserRole.User);

            _userRepository.Add(user);

            if (_unitOfWork.Commit())
            {
                _eventBus.RaiseEvent(new UserRegisteredEvent(user.Id, user.Name, user.Email));
            }            

            return Task.FromResult(new GenericCommandResult(true, "Usuário salvo com sucesso!", user.Id));
        }

        public Task<GenericCommandResult> Handle(UpdateUser command)
        {
            command.Validate();

            if (command.Invalid)
                return Task.FromResult(new GenericCommandResult(false, "Verifique mensagens", command.ValidationResult));

            var user = _userRepository.GetById(command.Id);

            if (user == null)
                return Task.FromResult(new GenericCommandResult(false, "Usuário não encontrado"));

            user.UpdateName(command.Name);

            _userRepository.Update(user);

            if (_unitOfWork.Commit())
            {
                _eventBus.RaiseEvent(new UserUpdatedEvent(user.Id, user.Name, user.Email));
            }

            return Task.FromResult(new GenericCommandResult(true, "Usuário atualizado com sucesso!", user));
        }

        public Task<GenericCommandResult> Handle(LoginUser command)
        {
            command.Validate();

            if (command.Invalid)
                return Task.FromResult(new GenericCommandResult(false, "Verifique mensagens", command.ValidationResult));

            var user = _userRepository.GetByEmail(command.Email);

            if (user != null)
            {
                if (user.Email == command.Email && user.Password == EncryptionHelper.Encrypt(command.Password))
                {
                    return Task.FromResult(new GenericCommandResult(true, "Usuário autenticado", user));
                }
            }

            return Task.FromResult(new GenericCommandResult(false, "Verifique credenciais de acesso"));
        }
    }
}
