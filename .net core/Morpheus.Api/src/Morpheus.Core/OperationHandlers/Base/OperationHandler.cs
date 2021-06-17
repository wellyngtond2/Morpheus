using MediatR;
using Morpheus.Common.Models;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Morpheus.Core.OperationHandlers.Base
{
    public abstract class OperationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : OperationRequest<TResponse>
        where TResponse : OperationResponse, new()
    {
        private readonly IUnitOfWork _unitOfWork;

        protected OperationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = new TResponse();

            try
            {
                var validations = ValidateOperation(request);

                if (validations.Any())
                {
                    if (validations.Any(report => report.Code == 404))
                    {
                        return OperationResponse.NotFound<TResponse>();
                    }

                    return OperationResponse.UnprocessableEntity<TResponse>(validations);
                }

                _unitOfWork?.BeginTransaction();

                response = await ProcessOperationAsync(request);

                if (response.Reports.Count == 0)
                    _unitOfWork?.CommitTransaction();
                else
                    _unitOfWork?.RollbackTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork?.RollbackTransaction();

                var report = Report.Create(500, ex.Message);

                return new TResponse
                {
                    State = OperationState.InternalServerError,
                    Reports = new Collection<Report> { report }
                };
            }

            return response;
        }

        protected abstract Task<TResponse> ProcessOperationAsync(TRequest request);

        protected abstract ICollection<Report> ValidateOperation(TRequest request);

    }
}