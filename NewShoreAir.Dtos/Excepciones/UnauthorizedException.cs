using System;

namespace NewShoreAir.Domain.Excepciones
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        { }
    }
}
