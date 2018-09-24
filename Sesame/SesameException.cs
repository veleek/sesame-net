using System;
using System.Net;

namespace Ben.CandyHouse
{
    /// <summary>
    /// An exception that is thrown by <see cref="SesameClient"/> when an error occurs.
    /// </summary>
    public class SesameException : Exception
    {
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
        /// <param name="statusCode">The status code of the HTTP request that resulted in this error.</param>
        public SesameException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public SesameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets the status code that was returned with the request.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

    }
}
