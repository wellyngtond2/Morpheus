using Bogus;
using Morpheus.DataContracts.Person;


namespace Morpheus.UnitTest.Fakes.Person
{
    public class CreatePersonOperationRequestFake : Faker<PersonRequest>
    {
        public CreatePersonOperationRequestFake()
        {
            RuleFor(it => it.Email, it => it.Person.Email);
            RuleFor(it => it.Name, it => it.Person.FirstName);
        }
    }
}
