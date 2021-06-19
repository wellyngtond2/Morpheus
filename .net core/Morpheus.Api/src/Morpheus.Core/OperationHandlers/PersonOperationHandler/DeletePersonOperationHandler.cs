using AutoMapper;
using Morpheus.Common.Messages;
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

        protected async override Task ValidateOperation(DeletePersonOperationRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
                Reports.Add(Report.Create(MessageNotification.InvalidId));

            var exists = await _unitOfWork.PersonRespository.ExistsByIdAsync(request.Id);

            if (!exists)
                Reports.Add(Report.Create(MessageNotification.PersonDoesNotExist));            
        }
    }
}