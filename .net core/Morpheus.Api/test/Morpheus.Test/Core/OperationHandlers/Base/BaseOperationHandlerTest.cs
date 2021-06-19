using AutoMapper;
using MediatR;
using Morpheus.Core.Mapper;
using Morpheus.Core.OperationHandlers.Base;
using Morpheus.Core.Repositories;
using Morpheus.DataContracts.Base;
using Morpheus.Test.Commons;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Morpheus.Test.Core.OperationHandlers.Base
{
    public class BaseOperationHandlerTest<TOperationHandler, TRequest, TResponse> : IClassFixture<MapperFixture<CoreProfile>>
        where TOperationHandler : OperationHandler<TRequest, TResponse>
        where TRequest : OperationRequest<TResponse>
        where TResponse : OperationResponse, new()
    {
        protected readonly IUnitOfWork UnitOfWorkMock;
        protected readonly IMapper Mapper;
        protected readonly IMediator BusMock;
        protected TRequest Request;
        protected TResponse Response;
        protected TOperationHandler OperationHandler;

        protected BaseOperationHandlerTest(MapperFixture<CoreProfile> mapperFixture)
        {
            UnitOfWorkMock = Substitute.For<IUnitOfWork>();
            BusMock = Substitute.For<IMediator>();
            Mapper = mapperFixture.Mapper;
        }

        protected async Task Act() => Response = await OperationHandler.Handle(Request, CancellationToken.None);
    }

    public abstract class BaseOperationHandlerTests<TOperationHandler, TRequest> : BaseOperationHandlerTest<TOperationHandler, TRequest, OperationResponse>
    where TOperationHandler : OperationHandler<TRequest, OperationResponse>
    where TRequest : OperationRequest<OperationResponse>
    {
        protected BaseOperationHandlerTests(MapperFixture<CoreProfile> mapperFixture) : base(mapperFixture) { }
    }
}