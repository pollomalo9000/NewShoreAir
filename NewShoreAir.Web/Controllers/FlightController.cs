using Microsoft.AspNetCore.Mvc;
using NewShore.Bussines;
using NewShore.DataAccess;
using NewShoreAir.Domain.Excepciones;

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
            var result = _flightBussines.GetRoutes(origin, destination, roundTrip);
            if (result == null || result.Count == 0)
            {
                throw new NotFoundException("su consulta no puede ser procesada");

            }


            return Ok(_flightBussines.GetRoutes(origin, destination, roundTrip));

        }
    }
}
