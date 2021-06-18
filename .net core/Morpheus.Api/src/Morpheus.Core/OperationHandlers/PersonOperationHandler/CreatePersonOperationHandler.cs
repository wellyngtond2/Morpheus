using AutoMapper;
using Morpheus.Common.Models;
using Morpheus.Core.Models;
using Morpheus.Core.OperationHandlers.Base;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpheus.Core.OperationHandlers.PersonOperationHandler
{
    public class CreatePersonOperationHandler : OperationHandler<CreatePersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public CreatePersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        protected async override Task<OperationResponse<PersonResponse>> ProcessOperationAsync(CreatePersonOperationRequest request)
        {
            var person = _mapper.Map<PersonModel>(request.Data);
            person.CreatedAt = DateTime.UtcNow;
            person.Id = Guid.NewGuid().ToString("N");

            await _unitOfWork.PersonRespository.CreateAsync(person);

            var response = _mapper.Map<PersonResponse>(person);

            return OperationResponse.Created(response);
        }

        protected override Task<ICollection<Report>> ValidateOperation(CreatePersonOperationRequest request)
        {
            ICollection<Report> response = new List<Report>();

            if (string.IsNullOrWhiteSpace(request.Data.Name))
                response.Add(Report.Create(400, "Name cannot be null or empty"));

            if (string.IsNullOrWhiteSpace(request.Data.Email))
                response.Add(Report.Create(400, "Email cannot be null or empty"));

            return Task.FromResult(response);
        }
    }
}