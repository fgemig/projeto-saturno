using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Contracts;
using System;

namespace Saturno.Infra
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SaturnoDataContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(SaturnoDataContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }      

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }        
    }
}
