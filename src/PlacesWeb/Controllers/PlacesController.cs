using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PlacesWeb.Models;
using PlacesWeb.Services;
using PlacesWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net;
//using UniversalWeb.Models;

namespace PlacesWeb.Controllers
{
    public class PlacesController : Controller
    {
        private readonly ILogger<PlacesController> _logger;
        private readonly IPlacesService _placesService;
        public PlacesController(ILogger<PlacesController> logger, IPlacesService placesService)
        {
            _logger = logger;
            _placesService = placesService;
        }

        public IActionResult Places()
        {
            List<Country> countriesList = new List<Country>();
            countriesList = Task.Run(async() => await _placesService.GetCountries()).Result.ToList<Country>();
            SelectList countrySelectList = new SelectList(countriesList, "CountryName", "CountryName");
            ViewBag.CountrySelectList = countrySelectList;
            return View();
        }

        // private IEnumerable<T> processResponse(HttpResponseMessage<IEnumerable<T>> httpResponseMessage)
        // {
        //     switch(httpResponseMessage.StatusCode)
        //     {
        //         case SuccessStatusCode:
        //     }
        // }

        [HttpGet]
        public JsonResult GetStatesForSelectList(string countryName)
        {
            // bool jsonIsValid = false;
            // StringBuilder jsonErrorMessage = new StringBuilder();
            List<State> statesList = new List<State>();
            statesList = Task.Run(async() => await _placesService.GetStates(countryName)).Result.ToList<State>();
            SelectList statesSelectList = new SelectList(statesList, "StateName", "StateName");
            return Json(statesSelectList);
        }

        [HttpGet]
        public JsonResult GetCitiesForSelectList(string stateName)
        {
            List<City> citiesList = new List<City>();
            citiesList = Task.Run(async() => await _placesService.GetCities(stateName)).Result.ToList<City>();
            SelectList citiesSelectList = new SelectList(citiesList, "CityName", "CityName");
            return Json(citiesSelectList);
        }

        private void logGeneralException(Exception exception)
        {
            Exception currentException = exception;
            while(currentException.InnerException != null)
            {
                currentException = currentException.InnerException;
                Console.WriteLine($"\nException Message: {currentException.Message}");
                _logger.LogError($"\nException Message: {currentException.Message} \n\nDetails: {currentException.StackTrace}");
            }
            
        }
    }
}
