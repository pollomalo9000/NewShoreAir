using System;

namespace NewShoreAir.Domain.Excepciones
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        { }
    }
}
