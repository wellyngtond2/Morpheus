using Morpheus.Core.Factory;
using Morpheus.Core.Models;
using Morpheus.Core.ValueObjects;

namespace Morpheus.ExternalServices.Services.Message.Factory
{
    public class MessageFactory : IMessageFactory
    {
        public Email CreateEmailNotificationNewUser(PersonModel person)
        {
            return new Email(person.Email, "wellyngton_borges@hotmail.com", "Your account was created with success", "Account created");
        }
    }
}
