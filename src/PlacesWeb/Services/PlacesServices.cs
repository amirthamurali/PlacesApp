using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PlacesWeb.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace PlacesWeb.Services
{
    public class PlacesService : IPlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public PlacesService(HttpClient httpClient, ILogger<PlacesService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var countriesResponse = await _httpClient.GetAsync("countries");
            IEnumerable<Country> countryEnumerable = new List<Country>();
            if(countriesResponse.IsSuccessStatusCode)
            {
                countryEnumerable = await countriesResponse.Content.ReadFromJsonAsync<IEnumerable<Country>>();
            }
            else
            {
                _logger.LogWarning($"Encountered {countriesResponse.StatusCode} for {countriesResponse}");
            }
            return countryEnumerable;
            
        }

        public async Task<IEnumerable<State>> GetStates(string countryName)
        {
            String uriEncodedCountryName = Uri.EscapeDataString(countryName);
            var statesResponse = await _httpClient.GetAsync($"states/{countryName}");
            IEnumerable<State> stateEnumerable = new List<State>();
            if(statesResponse.IsSuccessStatusCode)
            {
                stateEnumerable = await statesResponse.Content.ReadFromJsonAsync<IEnumerable<State>>();
            }
            else
            {
                _logger.LogWarning($"Encountered {statesResponse.StatusCode} for {countryName}");
            }
            return stateEnumerable;
        }

        public async Task<IEnumerable<City>> GetCities(string stateName)
        {
            var citiesResponse = await _httpClient.GetAsync($"cities/{stateName}");
            IEnumerable<City> cityEnumerable = new List<City>();
            if(citiesResponse.IsSuccessStatusCode)
            {
                cityEnumerable = await citiesResponse.Content.ReadFromJsonAsync<IEnumerable<City>>();
            }
            else
            {
                _logger.LogWarning($"Encountered {citiesResponse.StatusCode} for {stateName}");
            }
            return cityEnumerable;
        }
    }
}