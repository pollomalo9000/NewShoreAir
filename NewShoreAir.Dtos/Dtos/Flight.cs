namespace NewShoreAir.Domain.Dtos
{
    public class Flight
    {

        public Flight()
        {
            Transport = new Transport();
        }

        public Transport Transport { get; set; }

        public string? Origin { get; set; }

        public string? Destination { get; set; }

        public Double Price { get; set; }



    }
}