using MediatR;
using Saturno.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Saturno.Domain.Events.Handlers
{
    public class UserEventHandler : INotificationHandler<UserRegisteredEvent>, INotificationHandler<UserUpdatedEvent>
    {
        public Task Handle(UserRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(UserUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }
    }
}
