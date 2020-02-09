using Saturno.Domain.Enums;
using Saturno.Domain.Helpers;
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

            Password = EncryptionHelper.Encrypt(password);
        }

        public User(Guid id, string name, string email, UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public UserRole Role { get; set; }
        
        public void UpdateName(string newName)
            => Name = newName;

        public void SetUserRole(UserRole role) 
            => Role = role;
    }
}
