using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreAir.Domain.Dtos
{
    public class FlightDto
    {

        public string? DepartureStation { get; set; }
        public string? ArrivalStation { get; set; }
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
        public Double Price { get; set; }



    }
}
