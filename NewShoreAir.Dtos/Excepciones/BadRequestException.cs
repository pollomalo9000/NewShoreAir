using System;

namespace NewShoreAir.Domain.Excepciones
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
