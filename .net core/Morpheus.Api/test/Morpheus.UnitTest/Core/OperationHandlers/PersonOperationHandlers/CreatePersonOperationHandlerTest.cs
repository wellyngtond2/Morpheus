using FizzWare.NBuilder;
using FluentAssertions;
using Morpheus.Core.Mapper;
using Morpheus.Core.Models;
using Morpheus.Core.OperationHandlers.PersonOperationHandler;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using Morpheus.UnitTest.Commons;
using Morpheus.UnitTest.Core.OperationHandlers.Base;
using Morpheus.UnitTest.Fakes.Person;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Morpheus.UnitTest.Core.OperationHandlers.PersonOperationHandlers
{
    public class CreatePersonOperationHandlerTest : BaseOperationHandlerTest<CreatePersonOperationHandler, CreatePersonOperationRequest, OperationResponse<PersonResponse>>
    {
        public CreatePersonOperationHandlerTest(MapperFixture<CoreProfile> mapperFixture)
            : base(mapperFixture)
        {
            Request = new CreatePersonOperationRequest(new CreatePersonOperationRequestFake().Generate());

            OperationHandler = new CreatePersonOperationHandler(UnitOfWorkMock, Mapper);
        }

        [Fact]
        public async Task CreatePersonOperationHandler_ShouldValidateRequest()
        {
            // Act
            await Act();

            // Assert
            //Response.Reports.Any().Should().Be(false);
        }

        [Fact]
        public async Task CreatePersonOperationHandler_ShouldCreateANewPerson()
        {
            // Act
            await Act();

            // Arrange
            var person = Builder<PersonModel>.CreateNew().Build();

            await UnitOfWorkMock.PersonRespository.Received(1).CreateAsync(person);

            // Assert
            Response.State.Should().Be(OperationState.Created);
            Response.Reports.Should().BeEmpty();
            Response.Data.Should().NotBeNull();
        }
    }
}
