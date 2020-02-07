using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Entities;
using Saturno.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saturno.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaturnoDataContext context)
            : base(context) { }

        public async Task<User> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IReadOnlyCollection<User>> GetAll()
        {
            return await DbSet.AsNoTracking()
                .Select(c => new User(c.Id, c.Name, c.Email, c.Role)).ToListAsync();
        }
    }
}
