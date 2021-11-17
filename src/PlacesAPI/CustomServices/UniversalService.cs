using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using PlacesAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using PlacesAPI.Utils;
using Microsoft.Extensions.Logging;


namespace PlacesAPI.CustomServices
{
    public class UniversalService : IUniversalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private JsonSerializerOptions _jsonOptions;

        public UniversalService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;            
            _jsonOptions = new JsonSerializerOptions{ PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance };
        }

        private void FormulateRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add(_configuration["ServicesConfiguration:AcceptHeaderKey"],_configuration["ServicesConfiguration:AcceptHeaderValue"]);
        }

        private String EncodeRequestString(string requestParameter)
        {
           return Uri.EscapeDataString(requestParameter);
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            FormulateRequestHeaders();
            var response = await _httpClient.GetAsync("countries");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Country>>(_jsonOptions);
        }

        public async Task<IEnumerable<State>> GetStates(string countryName)
        {
            FormulateRequestHeaders();
            var response = await _httpClient.GetAsync($"states/{EncodeRequestString(countryName)}");
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadFromJsonAsync<IEnumerable<State>>(_jsonOptions);
        }

        public async Task<IEnumerable<City>> GetCities(string stateName)
        {
            FormulateRequestHeaders();
            var response = await _httpClient.GetAsync($"cities/{EncodeRequestString(stateName)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<City>>(_jsonOptions);
        }
    }
}