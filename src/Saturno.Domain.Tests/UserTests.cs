using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saturno.Domain.Commands;
using Saturno.Domain.Contracts;
using Saturno.Domain.Handlers;
using Saturno.Domain.Repositories;
using Saturno.Domain.Tests.Bus;
using Saturno.Domain.Tests.Repositories;
using Saturno.Domain.Tests.UoW;
using System;
using System.Threading.Tasks;

namespace Saturno.Domain.Tests
{
    [TestClass]
    public class UserTests
    {
        private static readonly IUserRepository _fakeUserRepository = new FakeUserRepository();
        private static readonly IEventBus _eventBus = new FakeEventBus();
        private static readonly IUnitOfWork _fakeUnitOfWork = new FakeUnityOfWork();
        private readonly UserHandler _handler = new UserHandler(_fakeUserRepository, _eventBus, _fakeUnitOfWork);

        private static RegisterUser NewValidUser() => new RegisterUser
        {
            Name = "Fabio",
            Email = "fabio@domain.com",
            Password = "123",
            ConfirmPassword = "123"
        };

        private static RegisterUser NewInvalidUser()
        {
            var invalidUser = NewValidUser();
            invalidUser.ConfirmPassword = string.Empty;

            return invalidUser;
        }

        private void ClearRepository()
        {
            var users = _fakeUserRepository.GetAll();

            foreach (var user in users)
                _fakeUserRepository.Remove(user.Id);
        }

        [TestInitialize()]
        public void Setup()
        {
            ClearRepository();
        }

        [TestMethod]
        public async Task Deve_Criar_Um_Novo_Usuario()
        {
            RegisterUser command = NewValidUser();

            var result = await _handler.Handle(command);

            var userGuid = (Guid)result.Data;

            var userDb = _fakeUserRepository.GetById(userGuid);

            Assert.AreEqual(0, command.ValidationResult.Count);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(userDb);
        }

        [TestMethod]
        public async Task Deve_Retornar_Notificacoes_Quando_Usuario_Invalido()
        {
            RegisterUser command = NewInvalidUser();

            var result = await _handler.Handle(command);

            Assert.AreEqual(2, command.ValidationResult.Count);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task Nao_Deve_Permitir_Atualizar_Um_Usuario_Inexistente()
        {
            var command = new UpdateUser
            {
                Id = Guid.NewGuid(),
                Name = "Fabio",
                Email = "fabio@domain.com"
            };

            var result = await _handler.Handle(command);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Usuário não encontrado", result.Message);
        }

        [TestMethod]
        public async Task Nao_Deve_Permitir_Atualizar_Quando_Usuario_Nao_Possuir_Um_ID()
        {
            var command = new UpdateUser
            {
                Name = "Fabio",
                Email = "fabio@domain.com"
            };

            var result = await _handler.Handle(command);

            Assert.AreEqual(1, command.ValidationResult.Count);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task Dado_Um_Novo_Novo_Deve_Atualizar_O_Nome_Existente()
        {
            RegisterUser registerUserCommand = NewValidUser();

            var registerUserCommandResult = await _handler.Handle(registerUserCommand);

            var userResultGuid = (Guid)registerUserCommandResult.Data;

            var userDb = _fakeUserRepository.GetById(userResultGuid);

            Assert.AreEqual(0, registerUserCommand.ValidationResult.Count);
            Assert.IsTrue(registerUserCommandResult.Success);
            Assert.IsNotNull(userDb);

            var updateUserCommand = new UpdateUser
            {
                Id = userDb.Id,
                Name = "Bruce",
                Email = userDb.Email
            };

            var updateUserCommandResult = await _handler.Handle(updateUserCommand);

            userDb = _fakeUserRepository.GetById(userResultGuid);

            Assert.AreEqual(0, updateUserCommand.ValidationResult.Count);
            Assert.IsTrue(updateUserCommandResult.Success);
            Assert.AreEqual("Bruce", userDb.Name);
        }
    }
}
