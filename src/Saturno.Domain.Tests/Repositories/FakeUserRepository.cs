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

        public Task Register(User user)
        {
            return Task.Run(() =>
            {
                users.Add(user);
            });
        }

        public Task Update(User user)
        {
            return Task.Run(() =>
            {
                var userDb = GetById(user.Id).Result;

                if (userDb != null)
                {
                    userDb.UpdateName(user.Name);
                }
            });            
        }

        public Task Delete(Guid id)
        {
            return Task.Run(() =>
            {
                var userDb = GetById(id).Result;

                if (userDb != null)
                {
                    users.Remove(userDb);
                }
            });
        }

        public Task<IReadOnlyCollection<User>> GetAll()
        {
            IReadOnlyCollection<User> list = users.ToList().AsReadOnly();

            return Task.Run(() =>
            {
                return list;
            });
        }

        public Task<User> GetById(Guid id)
        {
            return Task.Run(() =>
            {
                return users.FirstOrDefault(c => c.Id == id);
            });
        }       
    }
}
