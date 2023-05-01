using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreAir.Domain.Dtos
{
    public class Journey
    {


        public Journey()
        {
            Flights = new List<Flight>();
        }

        public List<Flight> Flights { get; set; }

        public string? Origin { get; set; }

        public string? Destination { get; set; }


        public Double Price
        {
            get { return Flights.Sum(c => c.Price); }

        }

        public  bool IsReturn { get; set; }




    }
}
