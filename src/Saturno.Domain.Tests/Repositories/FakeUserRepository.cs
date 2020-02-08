using Saturno.Domain.Entities;
using Saturno.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saturno.Domain.Tests.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>();

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Update(User user)
        {
            var userDb = GetById(user.Id);

            if (userDb != null)
            {
                userDb.UpdateName(user.Name);
            }
        }

        public void Remove(Guid id)
        {
            var userDb = GetById(id);

            if (userDb != null)
            {
                users.Remove(userDb);
            }
        }

        public IReadOnlyCollection<User> GetAll()
        {
            return  users.AsReadOnly();
        }

        public User GetById(Guid id)
        {
            return users.FirstOrDefault(c => c.Id == id);
        }

        public User GetByEmail(string email)
        {
            return users.FirstOrDefault(c => c.Email == email);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
