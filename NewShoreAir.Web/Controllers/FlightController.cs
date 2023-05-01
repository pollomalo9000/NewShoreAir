using Microsoft.AspNetCore.Mvc;
using NewShore.Bussines;
using NewShore.DataAccess;

namespace NewShoreAir.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        public IFlightBussines _flightBussines;

        public FlightController(IFlightBussines flightBussines)
        {
            _flightBussines = flightBussines;
        }

        [HttpGet("GetJourneys/{origin}/{destination}/{roundTrip}", Name = "GetClienteByCobis")]
        public async Task<IActionResult> GetJourneys(string origin, string destination,bool roundTrip)
        {
            return Ok(_flightBussines.GetRoutes(origin, destination, roundTrip));

        }
    }
}
