using AutoMapper;
using MediatR;
using Morpheus.Common.Messages;
using Morpheus.Common.Models;
using Morpheus.Core.Events;
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
        private readonly IMediator _mediator;

        public CreatePersonOperationHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }

        protected async override Task<OperationResponse<PersonResponse>> ProcessOperationAsync(CreatePersonOperationRequest request)
        {
            var person = _mapper.Map<PersonModel>(request.Data);
            person.CreatedAt = DateTime.UtcNow;
            person.Id = Guid.NewGuid().ToString("N");

            await _unitOfWork.PersonRespository.CreateAsync(person);

            await _mediator.Publish(new NewPersonEmailNotification(person));

            var response = _mapper.Map<PersonResponse>(person);

            return OperationResponse.Created(response);
        }

        protected override Task ValidateOperation(CreatePersonOperationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Data.Name))
                Reports.Add(Report.Create(MessageNotification.NameCannotBeNull));

            if (string.IsNullOrWhiteSpace(request.Data.Email))
                Reports.Add(Report.Create(MessageNotification.EmailCannotBeNull));

            return Task.FromResult(Reports);
        }
    }
}