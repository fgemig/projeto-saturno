using System;

namespace Saturno.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
