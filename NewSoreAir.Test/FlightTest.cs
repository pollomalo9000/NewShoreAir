using Microsoft.AspNetCore.Mvc;
using Moq;
using NewShore.Bussines;
using NewShoreAir.Domain.Dtos;
using NewShoreAir.Web.Controllers;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;
using Newtonsoft.Json.Linq;


namespace NewSoreAir.Test
{
    public class FlightTest
    {

        private readonly Mock<IFlightBussines> _flightBussines;


      
        public FlightTest()
        {
            this._flightBussines = new Mock<IFlightBussines>();
        }



      
        [Fact]
        public  void NoEncontroRutas()
        {

            this._flightBussines
          .Setup(x => x.GetRoutes(string.Empty,string.Empty,false)).Returns(new List<Journey>());

            FlightController sut = new FlightController(_flightBussines.Object);

            IActionResult result =  sut.GetJourneys(string.Empty,string.Empty,false).Result;
            BadRequestObjectResult objectResults = (BadRequestObjectResult)result;

            Assert.Equal("Missing ID", objectResults.Value);


            Assert.Equal((decimal)1, 1);
            


        }
    }
}