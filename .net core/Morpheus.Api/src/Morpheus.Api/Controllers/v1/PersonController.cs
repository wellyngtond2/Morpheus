using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morpheus.Api.Controllers.Base;
using Morpheus.DataContracts.Base;
using Morpheus.DataContracts.Person;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Morpheus.Api.Controllers.v1
{
    [Route("v1/people")]
    public class PersonController : ApiController
    {
        public PersonController(IMediator bus) : base(bus) { }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<OperationResponse<PersonResponse>>> Create([FromBody] PersonRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var operationRequest = new CreatePersonOperationRequest(request);
            var operationResponse = await Bus.Send(operationRequest);

            return TranslateResponse(operationResponse);
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse>> Update(string id, [FromBody] PersonRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var operationRequest = new UpdatePersonOperationRequest(id, request);
            var operationResponse = await Bus.Send(operationRequest);

            return TranslateResponse(operationResponse);
        }

        /// <summary>
        /// List of person
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("list")]
        public async Task<ActionResult<ListOperationResponse<PersonResponse>>> List([FromQuery] PersonFilter filter)
        {
            var operationRequest = new ListPersonOperationRequest(filter);
            var operationResponse = await Bus.Send(operationRequest);

            return TranslateResponse(operationResponse);
        }

        /// <summary>
        /// Get a person by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<PersonResponse>>> Get(string id)
        {
            var operationRequest = new GetPersonOperationRequest(id);
            var operationResponse = await Bus.Send(operationRequest);

            return TranslateResponse(operationResponse);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse>> Delete(string id)
        {
            var operationRequest = new DeletePersonOperationRequest(id);
            var operationResponse = await Bus.Send(operationRequest);

            return TranslateResponse(operationResponse);
        }
    }
}