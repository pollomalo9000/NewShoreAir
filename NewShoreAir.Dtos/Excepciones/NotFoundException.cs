using System;

namespace NewShoreAir.Domain.Excepciones
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }
}
