using Morpheus.Common.Models;
using Morpheus.Core.ValueObjects;
using System.Threading.Tasks;

namespace Morpheus.Core.Services
{
    public interface IEmailNotificationService
    {
        Task<ProcessResult> SendToQueueAsync(Email email);
    }
}
