using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlacesWeb.Models;
// using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace PlacesWeb.Services
{
    public interface IPlacesService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<State>> GetStates(string countryName);
        Task<IEnumerable<City>> GetCities(string stateName);
    }
}