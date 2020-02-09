using MediatR;
using System;

namespace Saturno.Domain.Common.Models
{
    public abstract class Event : IRequest, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
