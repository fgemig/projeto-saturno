using Saturno.Domain.Contracts;
using Saturno.Domain.Validation;
using System;

namespace Saturno.Domain.Commands
{
    public class DeleteUser : AbstractValidator, ICommand
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Specification()
                    .Requires()
                    .IsNotEmpty(Id, nameof(Id), "Id não informado")
                    .Compile()
            );
        }
    }
}
