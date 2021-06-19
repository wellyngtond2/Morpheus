using Morpheus.Common.Models;
using Morpheus.Core.Services;
using Morpheus.Core.ValueObjects;
using Morpheus.ExternalServices.Services.Message.Models;
using System.Threading.Tasks;

namespace Morpheus.ExternalServices.Services.Message
{
    public class RabbitMqService : MessageBase<Email>, IEmailNotificationService
    {
        public async Task<ProcessResult> SendToQueueAsync(Email email)
        {
            return await base.Send(new MessageTransport<Email>() { Data = email, Queue = "email_new_user" });
        }
    }
}
