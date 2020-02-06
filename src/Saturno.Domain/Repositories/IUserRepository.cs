using Saturno.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saturno.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Register(User user);
        Task Update(User user);
        Task Delete(Guid id);
        Task<User> GetById(Guid id);
        Task<IReadOnlyCollection<User>> GetAll();
    }
}
