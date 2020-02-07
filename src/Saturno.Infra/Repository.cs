using Microsoft.EntityFrameworkCore;
using Saturno.Domain.Contracts;
using System;
using System.Threading.Tasks;

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

        public async Task Add(TEntity obj)
        {
            DbSet.Add(obj);
            await Db.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Update(TEntity obj)
        {
            DbSet.Update(obj);
            await Db.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            await Db.SaveChangesAsync();
        }      

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }        
    }
}
