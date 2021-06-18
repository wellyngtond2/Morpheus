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
    public class UpdatePersonOperationHandler : OperationHandler<UpdatePersonOperationRequest, OperationResponse>
    {
        public UpdatePersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<OperationResponse> ProcessOperationAsync(UpdatePersonOperationRequest request)
        {
            var person = await _unitOfWork.PersonRespository.GetByIdAsync(request.Id);

            var updatePerson = _mapper.Map(request.Data, person);

            await _unitOfWork.PersonRespository.UpdateAsync(updatePerson);

            var response = _mapper.Map<PersonResponse>(updatePerson);

            return OperationResponse.Created(response);
        }

        protected async override Task<ICollection<Report>> ValidateOperation(UpdatePersonOperationRequest request)
        {
            var response = new List<Report>();

            var exists = await _unitOfWork.PersonRespository.ExistsByIdAsync(request.Id);

            if (!exists)
                response.Add(Report.Create(400, "Invalid person"));

            return response;
        }
    }
}