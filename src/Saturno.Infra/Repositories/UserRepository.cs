using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Entities;
using Saturno.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Saturno.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaturnoDataContext context)
            : base(context) { }

        public User GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public IReadOnlyCollection<User> GetAll()
        {
            return DbSet.AsNoTracking()
                .Select(c => new User(c.Id, c.Name, c.Email, c.Role)).ToList();
        }
    }
}
