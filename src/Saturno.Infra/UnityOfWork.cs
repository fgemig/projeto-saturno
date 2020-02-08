using Saturno.Domain.Contracts;

namespace Saturno.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SaturnoDataContext _context;

        public UnitOfWork(SaturnoDataContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
