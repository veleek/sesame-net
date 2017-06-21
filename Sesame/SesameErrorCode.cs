namespace Ben.CandyHouse
{
    /// <summary>
    /// Common Sesame error codes
    /// </summary>
    public enum SesameErrorCode
    {
        /// <summary>
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Required field cannot be empty
        /// </summary>
        MissingRequiredField = 10000,

        /// <summary>
        /// Email is invalid
        /// </summary>
        InvalidEmail = 10001,

        /// <summary>
        /// Wrong email or password
        /// </summary>
        IncorrectEmailOrPassword = 10002,

        /// <summary>
        /// Wrong control type; control must be “lock” or “unlock”
        /// </summary>
        InvalidControlType = 10005,

        /// <summary>
        /// Sesame not found
        /// </summary>
        SesameNotFound = 11002,

        /// <summary>
        /// Missing auth token
        /// </summary>
        AuthTokenMissing = 20001,

        /// <summary>
        /// Wrong auth token
        /// </summary>
        AuthTokenIncorrect = 20002,

        /// <summary>
        /// Auth token expired
        /// </summary>
        AuthTokenExpired = 20003,

        /// <summary>
        /// Auth token expired due to password change
        /// </summary>
        AuthTokenPasswordChanged = 20004,

        /// <summary>
        /// Auth user account has been deleted
        /// </summary>
        UserAccountDeleted = 20005,

        /// <summary>
        /// Owner permission required
        /// </summary>
        OwnerPermissionRequired = 30000,

        /// <summary>
        /// Account email has not been verified
        /// </summary>
        AccountNotVerified = 31000,

        /// <summary>
        /// Account is locked
        /// </summary>
        AccountLocked = 31001,

        /// <summary>
        /// Cloud API not enabled
        /// </summary>
        CloudApiNotEnabled = 32000,

        /// <summary>
        /// An internal error occurred.  Please report it to CandyHouse.
        /// </summary>
        InternalError = 50000,
    }
}