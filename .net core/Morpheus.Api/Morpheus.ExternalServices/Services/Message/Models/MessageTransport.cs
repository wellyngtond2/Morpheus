namespace Morpheus.ExternalServices.Services.Message.Models
{
    public class MessageTransport<TData>
    {
        public string Queue { get; set; }
        public TData Data { get; set; }
    }
}
