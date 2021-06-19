using Morpheus.Common.Models;
using Morpheus.Core.Services;
using Morpheus.Core.ValueObjects;
using Morpheus.ExternalServices.Services.Message.Models;
using System.Threading.Tasks;

namespace Morpheus.ExternalServices.Services.Message
{
    public class RabbitMqService : MessageBase<Email>, IEmailNotificationService
    {
        public const string QUEUE_NEW_EMAIL = "email_new_user";

        public async Task<ProcessResult> SendToQueueAsync(Email email)
        {
            return await base.Send(new MessageTransport<Email>() { Data = email, Queue = QUEUE_NEW_EMAIL });
        }
    }
}
