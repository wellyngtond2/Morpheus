using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class DeletePersonOperationRequest : OperationRequest<OperationResponse>
    {
        public DeletePersonOperationRequest(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
