using AutoMapper;
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
        protected IUnitOfWork _unitOfWork { get; }
        protected IMapper _mapper { get; }
        protected ICollection<Report> Reports { get; }

        protected OperationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Reports = new List<Report>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = new TResponse();

            try
            {
                await ValidateOperation(request);

                if (Reports.Any())
                {
                    if (Reports.Any(report => report.Code == 404))
                    {
                        return OperationResponse.NotFound<TResponse>();
                    }

                    return OperationResponse.UnprocessableEntity<TResponse>(Reports);
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

        protected abstract Task ValidateOperation(TRequest request);

    }
}