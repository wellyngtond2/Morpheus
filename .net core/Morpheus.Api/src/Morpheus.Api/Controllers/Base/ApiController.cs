using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morpheus.Common.Models;
using Morpheus.DataContracts.Base;
using System;

namespace Morpheus.Api.Controllers.Base
{
    public class ApiController : ControllerBase
    {
        // constructor
        protected ApiController(IMediator bus)
        {
            Bus = bus;
        }

        /// <summary>
        /// 
        /// </summary>
        protected IMediator Bus { get; }

        protected ActionResult<TOperationResponse> TranslateResponse<TOperationResponse>(TOperationResponse response)
            where TOperationResponse : OperationResponse
        {
            switch (response.State)
            {
                case OperationState.Undefined: throw new Exception("Operation state not defined.");
                case OperationState.Ok: return Ok(response);
                case OperationState.Created: return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
                case OperationState.NoContent: return NoContent();
                case OperationState.Accepted: return Accepted();
                case OperationState.NotFound: return NotFound();
                case OperationState.Unauthorized: return Unauthorized(response);
                case OperationState.Forbind: return new ObjectResult(response) { StatusCode = StatusCodes.Status403Forbidden };
                case OperationState.UnprocessableEntity: return UnprocessableEntity(response);
                case OperationState.InternalServerError: return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError };
                case OperationState.ServiceUnavailable: return StatusCode(StatusCodes.Status503ServiceUnavailable, response.Reports);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected new ObjectResult BadRequest()
        {
            var response = new OperationResponse { Reports = { Report.Create(400, "Request is null") } };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}