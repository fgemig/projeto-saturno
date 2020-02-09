using Saturno.Domain.Common.Models;
using System.Threading.Tasks;

namespace Saturno.Domain.Common.Bus
{
    public interface IEventBus
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
