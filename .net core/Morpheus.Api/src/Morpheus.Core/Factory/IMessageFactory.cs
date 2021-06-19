using Morpheus.Core.Models;
using Morpheus.Core.ValueObjects;

namespace Morpheus.Core.Factory
{
    public interface IMessageFactory
    {
        Email CreateEmailNotificationNewUser(PersonModel person);
    }
}
