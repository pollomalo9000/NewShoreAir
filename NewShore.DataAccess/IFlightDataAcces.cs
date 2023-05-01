using NewShoreAir.Domain.Dtos;

namespace NewShore.DataAccess
{
    public interface IFlightDataAcces
    {
         List<Flight> GetAll();

    }
}