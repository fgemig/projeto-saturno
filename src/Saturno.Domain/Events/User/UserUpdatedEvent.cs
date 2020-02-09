using Saturno.Domain.Models;
using System;

namespace Saturno.Domain.Events
{
    public class UserUpdatedEvent : Event
    {
        public UserUpdatedEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
