using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using TestStack.BDDfy;
using Moq;

using PlacesAPI.CustomServices;
using PlacesAPI.Controllers;
using PlacesAPI.Models;


namespace PlacesAPI.Test
{
    [Story(
        Title = "Read all cities for a given state, from the Universal REST API",
        AsA = "As an API Client",
        IWant = "I want to consume the service that lists all cities for a given state",
        SoThat = "So that I can view all available cities.")]

    public class ReadCities
    {
        private PlacesController  _controller;
        private Mock<IUniversalService> _mockUniversalService;

        private Task<IEnumerable<Country>> _mockResponseFromUniversalService;
        private IActionResult _controllerResult;

        private static readonly String _validRequestState1 = "Tamil Nadu";
        private static readonly List<City> _inMemoryCitiesListForState1 = new List<City>()
        {
            new City(){ CityName = "Coimbatore"},
            new City(){ CityName = "Chennai"},
            new City(){ CityName = "Madurai"},
        };

        [Fact]
        //Success scenario - Input is valid, and the entry exists in data storage
        public void SuccessfullyReadAllCities()
        {
                //Arrange
          this.Given(_ => GivenThatTheUniversalRestApiIsAvailableWithData())
                //Act
                .When(_ => WhenTheRequestForCitiesIsSentToPlacesApi())
                //Assert
                .Then(_ => ThenTheServiceMustReturnResultIndicatingSuccess())
                .And(_ => ThenTheReturnedResultTypeMustBeAListeOfStates())
                .And(_ => ThenTheNumberOfStatesReturnedMustMatchTheActualCount())
                .BDDfy();
                
        }

        [Fact]
        //Success scenario - Input is valid, and the entry exists in data storage
        public void NoStatesFoundInRecords()
        {
                //Arrange
          this.Given(_ => GivenThatTheUniversalRestApiReturnsAnEmptySetOfStates())
                //Act
                .When(_ => WhenTheRequestForCitiesIsSentToPlacesApi())
                //Assert
                .Then(_ => ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound())
                .BDDfy();
                
        }

        private void GivenThatTheUniversalRestApiIsAvailableWithData()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetCities(_validRequestState1)).ReturnsAsync(_inMemoryCitiesListForState1);
      }

        private void GivenThatTheUniversalRestApiReturnsAnEmptySetOfStates()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetCities(_validRequestState1)).ReturnsAsync(new List<City>());
        }

        private void WhenTheRequestForCitiesIsSentToPlacesApi()
        {
            _controller = new PlacesController(_mockUniversalService.Object);
            _controllerResult = _controller.GetCities(_validRequestState1);
        }

        private void ThenTheServiceMustReturnResultIndicatingSuccess()
        {
            Assert.IsType<OkObjectResult>(_controllerResult as OkObjectResult);
        }

        private void ThenTheReturnedResultTypeMustBeAListeOfStates()
        {
            Assert.IsType<List<City>>((_controllerResult as OkObjectResult).Value as List<City>);
        }

        private void ThenTheNumberOfStatesReturnedMustMatchTheActualCount()
        {
            Assert.Equal(((_controllerResult as OkObjectResult).Value as List<City>).Count, _inMemoryCitiesListForState1.Count);
        }

        private void ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound()
        {
            Assert.IsType<NotFoundResult>(_controllerResult as NotFoundResult);
        }
    }
}