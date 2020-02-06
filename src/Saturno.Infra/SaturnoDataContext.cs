using Microsoft.EntityFrameworkCore;

namespace Saturno.Infra
{
    public class SaturnoDataContext : DbContext
    {
        public SaturnoDataContext(DbContextOptions<SaturnoDataContext> options)
            : base(options) { }
    }
}
