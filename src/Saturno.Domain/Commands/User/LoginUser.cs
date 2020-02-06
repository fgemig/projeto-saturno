using Saturno.Domain.Commands.Contracts;
using Saturno.Domain.Validation;

namespace Saturno.Domain.Commands
{
    public class LoginUser : AbstractValidator, ICommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Specification()
                    .Requires()
                    .IsNotNullOrWhiteSpace(Email, nameof(Email), "E-mail não informado")
                    .IsNotNullOrWhiteSpace(Password, nameof(Password), "Senha não informadas")
                    .Compile()
            );
        }
    }
}
