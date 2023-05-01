using System;

namespace NewShoreAir.Domain.Excepciones
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        { }
    }
}
