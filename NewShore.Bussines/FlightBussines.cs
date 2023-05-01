using Microsoft.Extensions.Caching.Memory;
using NewShore.DataAccess;
using NewShoreAir.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShore.Bussines
{
    public class FlightBussines : IFlightBussines
    {
        public IFlightDataAcces _flightDataAcces;

        public FlightBussines(IFlightDataAcces flightDataAcces)
        {

            _flightDataAcces = flightDataAcces;
        }


        public List<Journey> GetRoutes(string origin, string destination, bool roundTrip)
        {

            List<Journey> routes = new List<Journey>();
            List<Journey> routesReturn = new List<Journey>();


            var fligths = _flightDataAcces.GetAll();

            var fligthsGo = fligths.Where(c => c.Transport.FlightNumber.StartsWith("8")).ToList();


            routes.AddRange(GetRoutesZeroStopovers(fligthsGo, origin, destination));
            routes.AddRange(GetRoutesOneStopover(fligthsGo, origin, destination));


            if (roundTrip)
            {
                var fligthsReturn = fligths.Where(c => c.Transport.FlightNumber.StartsWith("9")).ToList();
                routesReturn.AddRange(GetRoutesZeroStopovers(fligthsGo, origin, destination));
                routesReturn.AddRange(GetRoutesOneStopover(fligthsGo, origin, destination));
                routesReturn.ForEach(c => c.IsReturn = true);
                routes.AddRange(routesReturn);
            }

            return routes;



        }





        public List<Journey> GetRoutesZeroStopovers(List<Flight> flights, string origin, string destination)
        {
            List<Journey> journeys = new List<Journey>();

            foreach (var flight in flights)
            {

                Journey journey = new Journey
                {
                    Destination = destination,
                    Origin = origin,
                };

                if (flight.Origin == origin && flight.Destination == destination)
                    journey.Flights.Add(flight);

                if (journey.Flights.Count() > 0)
                journeys.Add(journey);
            
            }

            return journeys;

        }

        public List<Journey> GetRoutesOneStopover(List<Flight> flights, string origin, string destination)
        {
            List<Journey> journeys = new List<Journey>();

            foreach (var flight1 in flights)
            {
                foreach (var flight2 in flights)
                {
                    Journey journey = new Journey
                    {
                        Destination = destination,
                        Origin = origin,
                    };

                    if (flight1.Origin == origin && flight1.Destination == flight2.Origin && flight2.Destination == destination)
                    {
                        journey.Flights.Add(flight1);
                        journey.Flights.Add(flight2);
                    }
                    if (journey.Flights.Count() > 0)
                        journeys.Add(journey);

                }
              
            }

            return journeys;

        }




    }
}

