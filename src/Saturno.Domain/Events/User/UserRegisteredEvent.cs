using Saturno.Domain.Models;
using System;

namespace Saturno.Domain.Events
{
    public class UserRegisteredEvent : Event
    {
        public UserRegisteredEvent(Guid id, string name, string email)
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
