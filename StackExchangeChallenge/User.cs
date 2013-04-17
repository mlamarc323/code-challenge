using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication2
{
    
    public class User
    {

        [JsonProperty(PropertyName = "display_name")]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "reputation")]
        public int Reputation { get; internal set; }


    }
}