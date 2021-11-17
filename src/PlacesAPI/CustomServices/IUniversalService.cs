using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PlacesAPI.Models;

namespace PlacesAPI.CustomServices
{
    public interface IUniversalService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<State>> GetStates(string countryName);
        Task<IEnumerable<City>> GetCities(string stateName);
    }
}