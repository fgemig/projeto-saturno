using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Entities;

namespace Saturno.Infra
{
    public class SaturnoDataContext : DbContext
    {
        public SaturnoDataContext(DbContextOptions<SaturnoDataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
