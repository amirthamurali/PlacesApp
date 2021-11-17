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
        Title = "Read all countries from the external REST API",
        AsA = "As an API Client",
        IWant = "I want to consume the service that lists all countries",
        SoThat = "So that I can view all available countries in the system and select one to get its states")]

    public class ReadCountries
    {
        private PlacesController  _controller;
        private Mock<IUniversalService> _mockUniversalService;

        private Task<IEnumerable<Country>> _mockResponseFromUniversalService;
        private IActionResult _controllerResult;


        private static readonly List<Country> _inMemoryCountryList = new List<Country>{
                new Country(){ CountryName = "Albania" },
                new Country(){ CountryName = "Antarctica" },
                new Country(){ CountryName = "India" },
                new Country(){ CountryName = "Sri Lanka" },
                new Country(){ CountryName = "United States" }
            };

        [Fact]
        //Success scenario - Input is valid, and the entry exists in data storage
        public void SuccessfullyReadAllCountries()
        {
                //Arrange
          this.Given(_ => GivenThatTheUniversalRestApiIsAvailableWithData())
                //Act
                .When(_ => WhenTheRequestForCountriesIsSentToPlacesApi())
                //Assert
                .Then(_ => ThenTheServiceMustReturnResultIndicatingSuccess())
                .And(_ => ThenTheReturnedResultTypeMustBeAListOfCountries())
                .And(_ => ThenTheNumberOfCountriesReturnedMustMatchTheActualCount())
                .BDDfy();
                
        }

        [Fact]
        //Success scenario - Input is valid, and the entry exists in data storage
        public void NoCountriesFoundInRecords()
        {
                //Arrange
          this.Given(_ => GivenThatTheUniversalRestApiReturnsAnEmptySetOfCountries())
                //Act
                .When(_ => WhenTheRequestForCountriesIsSentToPlacesApi())
                //Assert
                .Then(_ => ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound())
                .BDDfy();
                
        }

        private void GivenThatTheUniversalRestApiIsAvailableWithData()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetCountries()).ReturnsAsync(_inMemoryCountryList);
        }

        private void GivenThatTheUniversalRestApiReturnsAnEmptySetOfCountries()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetCountries()).ReturnsAsync(new List<Country>());
        }

        private void WhenTheRequestForCountriesIsSentToPlacesApi()
        {
            _controller = new PlacesController(_mockUniversalService.Object);
            _controllerResult = _controller.GetCountries();
        }

        private void ThenTheServiceMustReturnResultIndicatingSuccess()
        {
            Assert.IsType<OkObjectResult>(_controllerResult as OkObjectResult);
        }

        private void ThenTheReturnedResultTypeMustBeAListOfCountries()
        {
            Assert.IsType<List<Country>>((_controllerResult as OkObjectResult).Value as List<Country>);
        }

        private void ThenTheNumberOfCountriesReturnedMustMatchTheActualCount()
        {
            Assert.Equal(((_controllerResult as OkObjectResult).Value as List<Country>).Count, _inMemoryCountryList.Count);
        }

        private void ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound()
        {
            Assert.IsType<NotFoundResult>(_controllerResult as NotFoundResult);
        }
    }
}