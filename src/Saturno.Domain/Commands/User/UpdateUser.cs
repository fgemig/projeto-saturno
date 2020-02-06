using Saturno.Domain.Commands.Contracts;
using Saturno.Domain.Validation;
using System;

namespace Saturno.Domain.Commands
{
    public class UpdateUser : AbstractValidator, ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }        

        public void Validate()
        {
            AddNotifications(
                new Specification()
                    .Requires()
                    .IsNotEmpty(Id, nameof(Id), "Id não informado")
                    .IsNotNullOrWhiteSpace(Name, nameof(Name), "O Nome é obrigatório")
                    .IsNotNullOrWhiteSpace(Email, nameof(Email), "O E-mail é obrigatório")
                    .Compile()
            );
        }
    }
}
