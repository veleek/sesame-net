namespace Ben.CandyHouse
{
    /// <summary>
    /// Extension methods to make working with error codes easier.
    /// </summary>
    public static class SesameErrorCodeExtensions
    {
        /// <summary>
        /// Indicates whether an error code is a bad request.
        /// </summary>
        /// <param name="errorCode">The error code to check.</param>
        /// <returns>True if the error code is a bad request error.</returns>
        public static bool IsBadRequest(this SesameErrorCode errorCode)
        {
            return errorCode.GetErrorClass() == 1;
        }

        /// <summary>
        /// Indicates whether an error code is an authorization error.
        /// </summary>
        /// <param name="errorCode">The error code to check.</param>
        /// <returns>True if the error code is an authorization error.</returns>
        public static bool IsAuthorizationError(this SesameErrorCode errorCode)
        {
            return errorCode.GetErrorClass() == 2;
        }

        /// <summary>
        /// Indicates whether an error code is a permissions error.
        /// </summary>
        /// <param name="errorCode">The error code to check.</param>
        /// <returns>True if the error code is a permissions error.</returns>
        public static bool IsPermissionsError(this SesameErrorCode errorCode)
        {
            return errorCode.GetErrorClass() == 3;
        }

        /// <summary>
        /// Indicates whether an error code is a server error.
        /// </summary>
        /// <param name="errorCode">The error code to check.</param>
        /// <returns>True if the error code is a server error.</returns>
        public static bool IsServerError(this SesameErrorCode errorCode)
        {
            return errorCode.GetErrorClass() == 5;
        }

        public static int GetErrorClass(this SesameErrorCode errorCode)
        {
            return (int)errorCode / 10000;
        }
    }
}