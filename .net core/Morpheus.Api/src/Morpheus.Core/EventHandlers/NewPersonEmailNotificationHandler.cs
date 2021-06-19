using MediatR;
using Morpheus.Core.Events;
using Morpheus.Core.Factory;
using Morpheus.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Morpheus.Core.EventHandlers
{
    public class NewPersonEmailNotificationHandler : INotificationHandler<NewPersonEmailNotification>
    {
        private readonly IEmailNotificationService _emailNotification;
        private readonly IMessageFactory _messageFactory;

        public NewPersonEmailNotificationHandler(IEmailNotificationService emailNotification, IMessageFactory messageFactory)
        {
            _emailNotification = emailNotification;
            _messageFactory = messageFactory;
        }

        public async Task Handle(NewPersonEmailNotification notification, CancellationToken cancellationToken)
        {
            var email = _messageFactory.CreateEmailNotificationNewUser(notification.Person);

            await _emailNotification.SendToQueueAsync(email);
        }
    }
}