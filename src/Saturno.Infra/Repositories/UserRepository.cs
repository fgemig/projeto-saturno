using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Entities;
using Saturno.Domain.Repositories;
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
    }
}
