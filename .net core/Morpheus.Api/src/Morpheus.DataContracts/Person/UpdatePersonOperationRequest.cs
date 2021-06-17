using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class UpdatePersonOperationRequest : OperationRequest<PersonRequest, OperationResponse>
    {
        public UpdatePersonOperationRequest(string id, PersonRequest data) : base(data)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
