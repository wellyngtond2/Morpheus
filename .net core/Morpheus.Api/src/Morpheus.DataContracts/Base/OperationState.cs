namespace Morpheus.DataContracts.Base
{
    public enum OperationState
    {
        Undefined = 0,
        Ok,
        Created,
        NoContent,
        Accepted,
        NotFound,
        UnprocessableEntity,
        InternalServerError,
        Unauthorized,
        Forbind,
        ServiceUnavailable,
        Redirect

    }
}
