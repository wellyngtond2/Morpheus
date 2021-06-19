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
    public class UpdatePersonOperationHandlerTest : BaseOperationHandlerTest<UpdatePersonOperationHandler, UpdatePersonOperationRequest, OperationResponse>
    {
        public UpdatePersonOperationHandlerTest(MapperFixture<CoreProfile> mapperFixture)
            : base(mapperFixture)
        {
            Request = new UpdatePersonOperationRequest("id", new CreatePersonOperationRequestFake().Generate());

            OperationHandler = new UpdatePersonOperationHandler(UnitOfWorkMock, Mapper);

            UnitOfWorkMock.PersonRespository.ExistsByIdAsync(Request.Id).Returns(true);
        }

        [Fact]
        public async Task UpdatePersonOperationHandler_ShouldValidateRequest()
        {
            // Act
            await Act();
        }

        [Fact]
        public async Task UpdatePersonOperationHandler_ShouldUpdateANewPerson()
        {
            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.NoContent);
            Response.Reports.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdatePersonOperationHandlerWithNullId_ShouldReturnInvalid()
        {
            Request = new UpdatePersonOperationRequest(null, new CreatePersonOperationRequestFake().Generate());

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(2);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.InvalidId);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.PersonDoesNotExist);
        }

        [Fact]
        public async Task UpdatePersonOperationHandlerWithNullName_ShouldReturnInvalid()
        {
            Request.Data.Name = null;

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(1);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.NameCannotBeNull);
        }

        [Fact]
        public async Task UpdatePersonOperationHandlerWithNullEmail_ShouldReturnInvalid()
        {
            Request.Data.Email = null;

            // Act
            await Act();

            // Assert
            Response.State.Should().Be(OperationState.UnprocessableEntity);
            Response.Reports.Count.Should().Be(1);
            Response.Reports.Should().Contain(c => c.Code == (int)MessageNotification.EmailCannotBeNull);
        }
    }
}
