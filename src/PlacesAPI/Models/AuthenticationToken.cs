using System;
using Newtonsoft.Json;

namespace PlacesAPI.Models
{
    public class AuthenticationTokenDTO
    {
        //[JsonProperty(PropertyName = "auth_token")]
        public string AuthToken { get; set; }
    }
}