using System;

namespace Ben.Sesame
{
    /// <summary>
    /// An exception that is thrown by <see cref="SesameClient"/> when an error occurs.
    /// </summary>
    public class SesameException : Exception
    {
        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="error">The error that caused this exception.</param>
        public SesameException(SesameError error) : this(error.Message)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        public SesameException(string message) : base(message)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public SesameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
