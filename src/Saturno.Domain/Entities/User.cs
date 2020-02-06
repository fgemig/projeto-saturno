using System;

namespace Saturno.Domain.Entities
{
    public class User : Entity<User>
    {
        protected User() { }

        public User(Guid id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public void UpdateName(string newName)
            => Name = newName;
    }
}
