using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlacesAPI.CustomServices;
using PlacesAPI.Models;

namespace PlacesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private IUniversalService _universalService;
        public PlacesController(IUniversalService universalService)
        {
            _universalService = universalService;
        }

        [HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
        {
            List<Country> countriesList = new List<Country>();
            countriesList = Task.Run(async() => await _universalService.GetCountries()).Result.ToList<Country>();
            if(countriesList.Count == 0)
                return NotFound();
            else
                return Ok(countriesList);
        }

        [HttpGet]
        [Route("states/{countryName}")]
        //public IEnumerable<Country> Get()
        public IActionResult GetStates(string countryName)
        {
            List<State> statesList = new List<State>();
            statesList = Task.Run(async() => await _universalService.GetStates(countryName)).Result.ToList<State>();
            if(statesList.Count == 0)
                return NotFound();
            else
                return Ok(statesList);
        }

        [HttpGet]
        [Route("cities/{stateName}")]
        public IActionResult GetCities(string stateName)
        {
            List<City> citiesList = new List<City>();
            citiesList = Task.Run(async() => await _universalService.GetCities(stateName)).Result.ToList<City>();
            if(citiesList.Count == 0)
                return NotFound();
            else
                return Ok(citiesList);
        }
    }
    
 }