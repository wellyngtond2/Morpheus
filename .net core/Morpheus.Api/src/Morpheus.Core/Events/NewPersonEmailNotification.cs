using MediatR;
using Morpheus.Core.Models;

namespace Morpheus.Core.Events
{
    public class NewPersonEmailNotification : INotification
    {
        public NewPersonEmailNotification(PersonModel person)
        {
            Person = person;
        }

        public PersonModel Person { get;}
    }
}
