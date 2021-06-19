using Newtonsoft.Json;

namespace Morpheus.Core.ValueObjects
{
    public class Email
    {
        public Email(string to, string from, string content, string subject)
        {
            To = to ?? "";
            From = from ?? "";
            Content = content ?? "";
            Text = subject ?? "";
        }

        [JsonProperty("to")]
        public string To { get; }
        [JsonProperty("from")]
        public string From { get; }
        [JsonProperty("content")]
        public string Content { get; }
        [JsonProperty("text")]
        public string Text { get; }
    }
}