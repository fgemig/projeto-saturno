using Saturno.Domain.Contracts;
using Saturno.Domain.Events;
using System;
using System.Threading.Tasks;

namespace Saturno.Domain.Tests.Bus
{
    public sealed class FakeEventBus : IEventBus
    {
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return Task.Run(() =>
                Console.WriteLine("Triggered event...")
            );
        }
    }
}
