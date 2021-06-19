using AutoMapper;
using Morpheus.Common.Models;
using Morpheus.Core.Filters;
using Morpheus.Core.OperationHandlers.Base;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpheus.Core.OperationHandlers.PersonOperationHandler
{
    public class ListPersonOperationHandler : OperationHandler<ListPersonOperationRequest, ListOperationResponse<PersonResponse>>
    {
        public ListPersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<ListOperationResponse<PersonResponse>> ProcessOperationAsync(ListPersonOperationRequest request)
        {
            var filter = _mapper.Map<PersonFilter>(request.Filter);

            var people = await _unitOfWork.PersonRespository.ListAsync(filter);

            var response = _mapper.Map<IEnumerable<PersonResponse>>(people);

            return OperationResponse.Ok(response);
        }

        protected override async Task ValidateOperation(ListPersonOperationRequest request)
        {
            await Task.CompletedTask;
        }
    }
}