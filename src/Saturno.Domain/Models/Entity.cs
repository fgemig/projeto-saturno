using System;

namespace Saturno.Domain.Models
{
    public abstract class Entity<T> where T : class
    {
        public Guid Id { get; protected set; }
    }
}