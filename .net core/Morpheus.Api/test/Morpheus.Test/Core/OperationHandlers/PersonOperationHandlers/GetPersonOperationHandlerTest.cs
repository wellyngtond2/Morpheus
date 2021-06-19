using FizzWare.NBuilder;
using FluentAssertions;
using Morpheus.Common.Extensions;
using Morpheus.Common.Messages;
using Morpheus.Core.Mapper;
using Morpheus.Core.Models;
using Morpheus.Core.OperationHandlers.PersonOperationHandler;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using Morpheus.Test.Commons;
using Morpheus.Test.Core.OperationHandlers.Base;
using Morpheus.Test.Fakes.Person;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Morpheus.Test.Core.OperationHandlers.PersonOperationHandlers
{
    public class GetPersonOperationHandlerTest : BaseOperationHandlerTest<GetPersonOperationHandler, GetPersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public GetPersonOperationHandlerTest(MapperFixture<CoreProfile> mapperFixture)
            : base(mapperFixture)
        {
            Request = new GetPersonOperationRequest("id");

            OperationHandler = new GetPersonOperationHandler(UnitOfWorkMock, Mapper);

            UnitOfWorkMock.PersonRespository.ExistsByIdAsync(Request.Id).Returns(true);
        }

        [Fact]
        public async Task GetPersonOperationHandler_ShouldValidateRequest()
        {
            // Act
            await Act();
        }

        [Fact]
        public async Task GetPersonOperationHandler_ShouldGetANewPerson()
        {
            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.Ok);
            Response.Reports.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPersonOperationHandlerWithNullId_ShouldReturnInvalid()
        {
            Request = new GetPersonOperationRequest(null);

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(2);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.InvalidId);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.PersonDoesNotExist);
        }
    }
}
