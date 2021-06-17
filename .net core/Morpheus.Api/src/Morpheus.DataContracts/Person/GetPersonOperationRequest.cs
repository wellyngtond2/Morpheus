using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class GetPersonOperationRequest : OperationRequest<OperationResponse<PersonResponse>>
    {
        public GetPersonOperationRequest(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}