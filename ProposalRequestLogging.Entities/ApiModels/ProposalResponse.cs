using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProposalRequestLogging.Models.Concrete
{
    public class Status
    {
        [JsonPropertyName("Value")]
        public string Value { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Status")]
        public Status Status { get; set; }
    }

    public class ProposalResponse
    {
        [JsonPropertyName("Results")]
        public List<Result> Results { get; set; }
    }
}