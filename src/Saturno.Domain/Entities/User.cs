using Saturno.Domain.Enums;
using System;
using System.Collections.Generic;

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

        public IReadOnlyCollection<UserRole> Roles => UserRoles;

        private List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public void UpdateName(string newName)
            => Name = newName;

        public void SetUserRole(UserRole role) 
            => UserRoles.Add(role);

        public void RemoveUserRole(UserRole role)
            => UserRoles.Remove(role);
    }
}
