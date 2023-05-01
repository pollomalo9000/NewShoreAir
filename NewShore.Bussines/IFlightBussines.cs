using NewShoreAir.Domain.Dtos;

namespace NewShore.Bussines
{
    public interface IFlightBussines
    {
        List<Journey> GetRoutes(string origin, string destination, bool roundTrip);
    }
}