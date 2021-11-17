using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PlacesAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using PlacesAPI.Utils;

namespace PlacesAPI.CustomServices
{
    public class UniversalAuthenticationService : IUniversalAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly IMemoryCache _memoryCache;
        private JsonSerializerOptions _jsonOptions;

        public UniversalAuthenticationService(HttpClient httpClient, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _jsonOptions = new JsonSerializerOptions{ PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance };
        }

        private void FormulateRequestHeader()
        {
            _httpClient.DefaultRequestHeaders.Add(_configuration["ServicesConfiguration:AcceptHeaderKey"],_configuration["ServicesConfiguration:AcceptHeaderValue"]);
            _httpClient.DefaultRequestHeaders.Add(_configuration["ServicesConfiguration:ApiTokenKey"],_configuration["ServicesConfiguration:ApiTokenValue"]);
            _httpClient.DefaultRequestHeaders.Add(_configuration["ServicesConfiguration:UserMailKey"],_configuration["ServicesConfiguration:UserMailValue"]);
        }

        public async Task<string> GetAuthenticationToken()
        {
            if (!_memoryCache.TryGetValue("Token", out string token))
            {
                FormulateRequestHeader();
                HttpResponseMessage response = await _httpClient.GetAsync("getaccesstoken");
                if(response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadFromJsonAsync<AuthenticationTokenDTO>(_jsonOptions);
                    token = responseBody.AuthToken;
                    _memoryCache.Set("Token",token);
                }
            }
            return token;
        }
    }
}