using Saturno.Domain.Events;
using System.Threading.Tasks;

namespace Saturno.Domain.Contracts
{
    public interface IEventBus
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
