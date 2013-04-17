using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication2
{
    [WrapperObject("items")]
    [JsonObject(MemberSerialization.OptIn)]
    public class Item
    {
        [JsonProperty(PropertyName = "question_id")]
        public int Id { get; internal set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; internal set; }

        [JsonProperty(PropertyName = "score")]
        public int Score { get; internal set; }
        
        [JsonProperty(PropertyName = "link")]
        public string Link { get; internal set; }

        [JsonProperty(PropertyName = "profile_image")]
        public string Gravitar { get; internal set; }
        
        [JsonProperty(PropertyName = "display_name")]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "reputation")]
        public int Reputation { get; internal set; }

        [JsonProperty(PropertyName = "answer_count")]
        public string AnswerCount { get; internal set; }

        
    }
}