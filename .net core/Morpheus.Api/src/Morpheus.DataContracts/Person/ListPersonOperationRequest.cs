using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class ListPersonOperationRequest : OperationRequest<ListOperationResponse<PersonResponse>>
    {
        public ListPersonOperationRequest(PersonFilter filter)
        {
            Filter = filter;
        }

        public PersonFilter Filter { get; set; }
    }
}