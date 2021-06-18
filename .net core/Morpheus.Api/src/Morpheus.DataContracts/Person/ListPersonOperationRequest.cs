using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class ListPersonOperationRequest : OperationRequest<ListOperationResponse<PersonResponse>>
    {
        public ListPersonOperationRequest(PersonFilterRequest filter)
        {
            Filter = filter;
        }

        public PersonFilterRequest Filter { get; set; }
    }
}