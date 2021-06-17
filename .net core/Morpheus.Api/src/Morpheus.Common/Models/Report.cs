using Newtonsoft.Json;

namespace Morpheus.Common.Models
{
    public sealed class Report
    {
        [JsonConstructor]
        private Report(int code, string field, string message)
        {
            Code = code;
            Field = field;
            Message = message;
        }

        public int Code { get; }
        public string Field { get; }
        public string Message { get; }

        public static Report Create(int code, string message) => new Report(code, null, message);

        public static Report Create(int code, string field, string message) => new Report(code, field, message);
    }
}
