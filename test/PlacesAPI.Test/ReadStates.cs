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
        Title = "Read all states from the external REST API",
        AsA = "As an API Client",
        IWant = "I want to consume the service that lists all states for a country",
        SoThat = "So that I can view all available records and select one to get its states")]

    public class ReadStates
    {
        private PlacesController  _controller;
        private Mock<IUniversalService> _mockUniversalService;

        private Task<IEnumerable<Country>> _mockResponseFromUniversalService;
        private IActionResult _controllerResult;

        private static readonly String _validRequestCountry1 = "India";
        private static readonly List<State> _inMemoryStatesListForCountry1 = new List<State>{
                new State(){ StateName = "Chandigarh" },
                new State(){ StateName = "Karnataka" },
                new State(){ StateName = "Kerala" },
                new State(){ StateName = "Telangana" },
                new State(){ StateName = "Tamil Nadu" }
            };

        [Fact]
        //Success scenario - Input is valid, and the entry exists in data storage
        public void SuccessfullyReadAllStates()
        {
                //Arrange
          this.Given(_ => GivenThatTheUniversalRestApiIsAvailableWithData())
                //Act
                .When(_ => WhenTheRequestForStatesIsSentToPlacesApi())
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
                .When(_ => WhenTheRequestForStatesIsSentToPlacesApi())
                //Assert
                .Then(_ => ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound())
                .BDDfy();
                
        }

        private void GivenThatTheUniversalRestApiIsAvailableWithData()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetStates(_validRequestCountry1)).ReturnsAsync(_inMemoryStatesListForCountry1);
      }

        private void GivenThatTheUniversalRestApiReturnsAnEmptySetOfStates()
        {
            _mockUniversalService = new Mock<IUniversalService>();
            _mockUniversalService.Setup(service => service.GetStates(_validRequestCountry1)).ReturnsAsync(new List<State>());
        }

        private void WhenTheRequestForStatesIsSentToPlacesApi()
        {
            _controller = new PlacesController(_mockUniversalService.Object);
            _controllerResult = _controller.GetStates(_validRequestCountry1);
        }

        private void ThenTheServiceMustReturnResultIndicatingSuccess()
        {
            Assert.IsType<OkObjectResult>(_controllerResult as OkObjectResult);
        }

        private void ThenTheReturnedResultTypeMustBeAListeOfStates()
        {
            Assert.IsType<List<State>>((_controllerResult as OkObjectResult).Value as List<State>);
        }

        private void ThenTheNumberOfStatesReturnedMustMatchTheActualCount()
        {
            Assert.Equal(((_controllerResult as OkObjectResult).Value as List<State>).Count, _inMemoryStatesListForCountry1.Count);
        }

        private void ThenTheServiceMustReturnResultIndicatingThatNoRecordsWereFound()
        {
            Assert.IsType<NotFoundResult>(_controllerResult as NotFoundResult);
        }
    }
}