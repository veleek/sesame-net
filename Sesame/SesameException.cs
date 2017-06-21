using System;

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
        /// <param name="error">The error that caused this exception.</param>
        public SesameException(SesameError error) : this(error.Message)
        {
            this.Code = error.Code;
        }

        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        public SesameException(string message) : base(message)
        {
            this.Code = SesameErrorCode.Unknown;
        }

        /// <summary>
        /// Create a new instance of <see cref="SesameException"/>
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public SesameException(string message, Exception innerException) : base(message, innerException)
        {
            this.Code = SesameErrorCode.Unknown;
        }

        /// <summary>
        /// Gets the error code associated with this exception.
        /// </summary>
        public SesameErrorCode Code { get; set; }
    }
}
