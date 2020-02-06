using Saturno.Domain.Contracts;
using Saturno.Domain.Entities;
using System.Threading.Tasks;

namespace Saturno.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
