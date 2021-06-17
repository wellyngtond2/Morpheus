using System.Collections.Generic;

namespace Morpheus.DataContracts.Base
{
    public class ListOperationResponse<T> : OperationResponse
    {
        public ListOperationResponse() { }

        public ListOperationResponse(IEnumerable<T> data, OperationState state)
            : base(state)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; }
    }
}

