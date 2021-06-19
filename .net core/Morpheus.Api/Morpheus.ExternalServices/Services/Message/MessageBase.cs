using EasyNetQ;
using Morpheus.Common.Models;
using Morpheus.ExternalServices.Services.Message.Models;
using System;
using System.Threading.Tasks;

namespace Morpheus.ExternalServices.Services.Message
{
    public abstract class MessageBase<T>
    {
        private readonly IBus _bus;

        public MessageBase()
        {
            _bus = RabbitHutch.CreateBus("host=localhost");
        }

        protected async Task<ProcessResult> Send(MessageTransport<T> message)
        {
            try
            {
                await _bus.SendReceive.SendAsync(message.Queue, message.Data);

                return ProcessResult.Success();
            }
            catch (Exception)
            {
                return ProcessResult.Failure(Report.Create(500, "Failure to send message queue"));
            }
        }
    }
}
