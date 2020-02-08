using MediatR;
using Saturno.Domain.Contracts;
using Saturno.Domain.Events;
using System.Threading.Tasks;

namespace Saturno.Infra.Bus
{
    public sealed class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
