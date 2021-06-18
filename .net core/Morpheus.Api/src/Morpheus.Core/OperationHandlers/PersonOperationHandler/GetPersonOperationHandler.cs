using AutoMapper;
using Morpheus.Common.Models;
using Morpheus.Core.OperationHandlers.Base;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpheus.Core.OperationHandlers.PersonOperationHandler
{
    public class GetPersonOperationHandler : OperationHandler<GetPersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public GetPersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<OperationResponse<PersonResponse>> ProcessOperationAsync(GetPersonOperationRequest request)
        {
            var person = await _unitOfWork.PersonRespository.GetByIdAsync(request.Id);

            var response = _mapper.Map<PersonResponse>(person);

            return OperationResponse.Ok(response);
        }

        protected async override Task<ICollection<Report>> ValidateOperation(GetPersonOperationRequest request)
        {
            var response = new List<Report>();

            var exists = await _unitOfWork.PersonRespository.ExistsByIdAsync(request.Id);

            if (!exists)
                response.Add(Report.Create(400, "Invalid person"));

            return response;
        }
    }
}