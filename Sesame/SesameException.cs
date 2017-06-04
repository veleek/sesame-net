using System;

namespace Ben.Sesame
{
    public class SesameException : Exception
    {
        public SesameException(SesameError error) : this(error.Message)
        {
        }

        public SesameException(string message) : base(message)
        {
        }

        public SesameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
