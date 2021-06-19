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
    public class UpdatePersonOperationHandler : OperationHandler<UpdatePersonOperationRequest, OperationResponse>
    {
        public UpdatePersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<OperationResponse> ProcessOperationAsync(UpdatePersonOperationRequest request)
        {
            var person = await _unitOfWork.PersonRespository.GetByIdAsync(request.Id);

            var updatePerson = _mapper.Map(request.Data, person);

            await _unitOfWork.PersonRespository.UpdateAsync(updatePerson);

            var response = _mapper.Map<PersonResponse>(updatePerson);

            return OperationResponse.NoContent();
        }

        protected async override Task ValidateOperation(UpdatePersonOperationRequest request)
        {
            if(string.IsNullOrEmpty(request.Id))
                Reports.Add(Report.Create(MessageNotification.InvalidId));

            var exists = await _unitOfWork.PersonRespository.ExistsByIdAsync(request.Id);

            if (!exists)
                Reports.Add(Report.Create(MessageNotification.PersonDoesNotExist));

            if (string.IsNullOrWhiteSpace(request.Data.Name))
                Reports.Add(Report.Create(MessageNotification.NameCannotBeNull));

            if (string.IsNullOrWhiteSpace(request.Data.Email))
                Reports.Add(Report.Create(MessageNotification.EmailCannotBeNull));
        }
    }
}