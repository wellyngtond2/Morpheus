using Morpheus.DataContracts.Base;

namespace Morpheus.DataContracts.Person
{
    public sealed class CreatePersonOperationRequest : OperationRequest<PersonRequest, OperationResponse<PersonResponse>>
    {
        public CreatePersonOperationRequest(PersonRequest data) : base(data)
        {
        }
    }
}
