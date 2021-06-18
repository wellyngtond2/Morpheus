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
    public class DeletePersonOperationHandler : OperationHandler<DeletePersonOperationRequest, OperationResponse>
    {
        public DeletePersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<OperationResponse> ProcessOperationAsync(DeletePersonOperationRequest request)
        {
            var person = await _unitOfWork.PersonRespository.GetByIdAsync(request.Id);

            await _unitOfWork.PersonRespository.DeleteAsync(person);

            return OperationResponse.NoContent();
        }

        protected async override Task<ICollection<Report>> ValidateOperation(DeletePersonOperationRequest request)
        {
            var response = new List<Report>();

            var exists = await _unitOfWork.PersonRespository.ExistsByIdAsync(request.Id);

            if (!exists)
                response.Add(Report.Create(400, "Invalid person"));

            return response;
        }
    }
}