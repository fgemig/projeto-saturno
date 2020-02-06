using Saturno.Domain.Commands.Contracts;
using Saturno.Domain.Validation;

namespace Saturno.Domain.Commands
{
    public class RegisterUser : AbstractValidator, ICommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Specification()
                    .Requires()
                    .IsNotNullOrWhiteSpace(Name, nameof(Name), "O Nome é obrigatório")
                    .IsNotNullOrWhiteSpace(Email, nameof(Email), "O E-mail é obrigatório")
                    .IsNotNullOrWhiteSpace(Password, nameof(Password), "A Senha é obrigatória")
                    .IsNotNullOrWhiteSpace(ConfirmPassword, nameof(ConfirmPassword), "Confirme a Senha de acesso")
                    .IsNotEquals(Password, ConfirmPassword, nameof(ConfirmPassword), "A senhas são diferentes")
                    .Compile()
            );
        }
    }
}
