using Saturno.Domain.Contracts;
using Saturno.Domain.Entities;
using System.Collections.Generic;

namespace Saturno.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        IReadOnlyCollection<User> GetAll();
    }
}
