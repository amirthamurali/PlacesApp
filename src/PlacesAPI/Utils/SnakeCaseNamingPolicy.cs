using System;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace PlacesAPI.Utils
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        private static readonly char _DELIMITER = '_';
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        { 
            return string.Concat(name.Select(c => Char.IsUpper(c) ? (String.Concat(_DELIMITER, Char.ToLower(c))) : c.ToString() )).TrimStart(_DELIMITER);
        }

    }
}