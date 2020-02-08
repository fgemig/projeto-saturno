using System;

namespace Saturno.Domain.Entities
{
    public abstract class Entity<T> where T : class
    {
        public Guid Id { get; protected set; }
    }
}