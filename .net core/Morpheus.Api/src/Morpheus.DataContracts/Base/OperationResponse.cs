using Morpheus.Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Morpheus.DataContracts.Base
{
    public class OperationResponse
    {
        public OperationResponse()
        {
            Reports = new List<Report>();
        }

        public OperationResponse(OperationState state, ICollection<Report> reports = null)
        {
            State = state;
            Reports = reports ?? new List<Report>();
        }

        public OperationResponse(OperationState state, Report report) : this(state, new List<Report> { report }) { }

        [JsonIgnore]
        public OperationState State { get; set; }
        public ICollection<Report> Reports { get; set; }

        /// <summary>
        /// Create instance of <see cref="OperationResponse"/> with property State "Ok".
        /// </summary>
        /// <returns>Instance of <see cref="OperationResponse"/></returns>
        public static OperationResponse<T> Ok<T>(T data) => new OperationResponse<T>(data, OperationState.Ok);

        public static ListOperationResponse<TData> Ok<TData>(IEnumerable<TData> data) =>
            new ListOperationResponse<TData>(data, OperationState.Ok);

        public static OperationResponse Ok() => new OperationResponse(OperationState.Ok);

        public static OperationResponse Created() => new OperationResponse(OperationState.Created);

        public static OperationResponse<T> Created<T>(T data) => new OperationResponse<T>(data, OperationState.Created);

        public static OperationResponse NoContent() => new OperationResponse(OperationState.NoContent);

        public static TResponse NotFound<TResponse>() where TResponse : OperationResponse, new() =>
            new TResponse { State = OperationState.NotFound };

        public static TResponse UnprocessableEntity<TResponse>(ICollection<Report> reports) where TResponse : OperationResponse, new() =>
            new TResponse { State = OperationState.UnprocessableEntity, Reports = reports };

        public static OperationResponse UnprocessableEntity(ICollection<Report> reports) =>
            new OperationResponse(OperationState.UnprocessableEntity, reports);

        public static OperationResponse InternalServerError(ICollection<Report> reports = null) =>
            new OperationResponse(OperationState.InternalServerError, reports);

    }

    public class OperationResponse<T> : OperationResponse
    {
        public OperationResponse() { }

        internal OperationResponse(T data, OperationState state, ICollection<Report> reports = null)
            : base(state, reports)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
