using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saturno.Domain.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IReadOnlyCollection<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(Guid id);
    }
}
