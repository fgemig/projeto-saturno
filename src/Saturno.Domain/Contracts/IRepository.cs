﻿using System;

namespace Saturno.Domain.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        void Update(TEntity obj);
        void Remove(Guid id);
    }
}
