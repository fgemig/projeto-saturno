using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saturno.Domain.Commands;
using Saturno.Domain.Entities;
using Saturno.Domain.Handlers;
using Saturno.Domain.Repositories;
using Saturno.Domain.Tests.Repositories;
using System;
using System.Threading.Tasks;

namespace Saturno.Domain.Tests
{
    [TestClass]
    public class UserTests
    {
        private static readonly IUserRepository _fakeUserRepository = new FakeUserRepository();
        private readonly UserHandler _handler = new UserHandler(_fakeUserRepository);

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

        private async void ClearRepository()
        {
            var users = await _fakeUserRepository.GetAll();

            foreach (var user in users)
                await _fakeUserRepository.Delete(user.Id);
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

            var result = (GenericCommandResult)await _handler.Handle(command);

            var user = (User)result.Data;

            var userDb = await _fakeUserRepository.GetById(user.Id);

            Assert.AreEqual(0, command.ValidationResult.Count);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(userDb);
        }        

        [TestMethod]
        public async Task Deve_Retornar_Notificacoes_Quando_Usuario_Invalido()
        {
            RegisterUser command = NewInvalidUser();

            var result = (GenericCommandResult)await _handler.Handle(command);

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

            var result = (GenericCommandResult)await _handler.Handle(command);

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

            var result = (GenericCommandResult)await _handler.Handle(command);

            Assert.AreEqual(1, command.ValidationResult.Count);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task Dado_Um_Novo_Novo_Deve_Atualizar_O_Nome_Existente()
        {
            RegisterUser registerUserCommand = NewValidUser();

            var registerUserCommandResult = (GenericCommandResult)await _handler.Handle(registerUserCommand);

            var userResult = (User)registerUserCommandResult.Data;

            var userDb = await _fakeUserRepository.GetById(userResult.Id);

            Assert.AreEqual(0, registerUserCommand.ValidationResult.Count);
            Assert.IsTrue(registerUserCommandResult.Success);
            Assert.IsNotNull(userDb);

            var updateUserCommand = new UpdateUser
            {
                Id = userDb.Id,
                Name = "Bruce",
                Email = userDb.Email
            };

            var updateUserCommandResult = (GenericCommandResult)await _handler.Handle(updateUserCommand);

            userDb = await _fakeUserRepository.GetById(userResult.Id);

            Assert.AreEqual(0, updateUserCommand.ValidationResult.Count);
            Assert.IsTrue(updateUserCommandResult.Success);
            Assert.AreEqual("Bruce", userDb.Name);
        }
    }
}
