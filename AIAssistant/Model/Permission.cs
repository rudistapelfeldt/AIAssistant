using System;
using Newtonsoft.Json;

namespace AIAssistant.Model
{
    public class Permission
    {         /// <summary>
              /// Permission Id (not to be confused with ModelId)
              /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Object type, should always be 'model_permission'
        /// </summary>
        [JsonProperty("object")]
        public string Object { get; set; }

        /// The time when the permission was created
        [JsonIgnore]
        public DateTime Created => DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime).DateTime;

        /// <summary>
        /// Unix timestamp for creation date/time
        /// </summary>
        [JsonProperty("created")]
        public long CreatedUnixTime { get; set; }

        /// <summary>
        /// Can the model be created?
        /// </summary>
        [JsonProperty("allow_create_engine")]
        public bool AllowCreateEngine { get; set; }

        /// <summary>
        /// Does the model support temperature sampling?
        /// https://beta.openai.com/docs/api-reference/completions/create#completions/create-temperature
        /// </summary>
        [JsonProperty("allow_sampling")]
        public bool AllowSampling { get; set; }

        /// <summary>
        /// Does the model support logprobs?
        /// https://beta.openai.com/docs/api-reference/completions/create#completions/create-logprobs
        /// </summary>
        [JsonProperty("allow_logprobs")]
        public bool AllowLogProbs { get; set; }

        /// <summary>
        /// Does the model support search indices?
        /// </summary>
        [JsonProperty("allow_search_indices")]
        public bool AllowSearchIndices { get; set; }

        [JsonProperty("allow_view")]
        public bool AllowView { get; set; }

        /// <summary>
        /// Does the model allow fine tuning?
        /// https://beta.openai.com/docs/api-reference/fine-tunes
        /// </summary>
        [JsonProperty("allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }

        /// <summary>
        /// Is the model only allowed for a particular organization? May not be implemented yet.
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

        /// <summary>
        /// Is the model part of a group? Seems not implemented yet. Always null.
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("is_blocking")]
        public bool IsBlocking { get; set; }
    }
}

