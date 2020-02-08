using Saturno.Domain.Contracts;

namespace Saturno.Domain.Tests.UoW
{
    public class FakeUnityOfWork : IUnitOfWork
    {       
        public bool Commit()
        {
            return true;
        }

        public void Dispose()
        {
            
        }
    }
}
