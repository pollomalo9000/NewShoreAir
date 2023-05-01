

using NewShoreAir.Domain.Automapper;
using NewShoreAir.Domain.Dtos;
using NewShoreAir.Domain.Enum;
using NewShoreAir.Domain.Excepciones;
using NewShoreAir.Domain.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NewShore.DataAccess
{
    public class FlightDataAcces : IFlightDataAcces
    {




        public List<Flight> GetAll()
        {



            List<FlightDto> dto =  HelperCache.GetItem("AllFlight", () => 
            {

                RestResponse resultado = HelperApi.Get<FlightDto>("flights/2", EnumApi.API_NEW_SHORE_FLIGHTS).Result;

                List<FlightDto> flights = new();
                if (resultado.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = resultado.Content ?? throw new NotFoundException("No encontrado");

                    JsonConvert.PopulateObject(resultado.Content, flights);
                    return flights;
                }
                throw new BadRequestException(resultado.Content ?? string.Empty);
              
            });


            return Mappers.Mapper.Map<List<FlightDto>, List<Flight>>(dto); ;

        }

    }
}
