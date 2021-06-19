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
    public class CreatePersonOperationHandlerTest : BaseOperationHandlerTest<CreatePersonOperationHandler, CreatePersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public CreatePersonOperationHandlerTest(MapperFixture<CoreProfile> mapperFixture)
            : base(mapperFixture)
        {
            Request = new CreatePersonOperationRequest(new CreatePersonOperationRequestFake().Generate());

            OperationHandler = new CreatePersonOperationHandler(UnitOfWorkMock, Mapper, BusMock);
        }

        [Fact]
        public async Task CreatePersonOperationHandler_ShouldValidateRequest()
        {
            // Act
            await Act();
        }

        [Fact]
        public async Task CreatePersonOperationHandler_ShouldCreateANewPerson()
        {
            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.Created);
            Response.Reports.Should().BeEmpty();
            Response.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task CreatePersonOperationHandlerWithNullName_ShouldReturnInvalid()
        {
            Request.Data.Name = null;

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(1);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.NameCannotBeNull);
            Response.Data.Should().BeNull();
        }

        [Fact]
        public async Task CreatePersonOperationHandlerWithNullEmail_ShouldReturnInvalid()
        {
            Request.Data.Email = null;

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(1);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.EmailCannotBeNull);
            Response.Data.Should().BeNull();
        }
    }
}
