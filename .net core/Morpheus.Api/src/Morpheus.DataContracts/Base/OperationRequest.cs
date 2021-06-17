using MediatR;

namespace Morpheus.DataContracts.Base
{
    public class OperationRequest<TOperationResponse> : IRequest<TOperationResponse> where TOperationResponse : OperationResponse { }
    
    public class OperationRequest<TData, TOperationResponse> : OperationRequest<TOperationResponse>
       where TOperationResponse : OperationResponse
    {
        public OperationRequest(TData data)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}
