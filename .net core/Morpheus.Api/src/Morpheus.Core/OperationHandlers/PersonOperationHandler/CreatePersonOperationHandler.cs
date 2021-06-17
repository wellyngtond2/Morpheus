using Morpheus.Common.Models;
using Morpheus.Core.OperationHandlers.Base;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpheus.Core.OperationHandlers.PersonOperationHandler
{
    public class CreatePersonOperationHandler : OperationHandler<CreatePersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public CreatePersonOperationHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override Task<OperationResponse<PersonResponse>> ProcessOperationAsync(CreatePersonOperationRequest request)
        {
            throw new NotImplementedException();
        }

        protected override ICollection<Report> ValidateOperation(CreatePersonOperationRequest request)
        {
            var response = new List<Report>();

            return response;
        }
    }
}