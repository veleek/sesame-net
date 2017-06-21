using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The request body for a login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The email address of the account to authenticate as.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// The password for the account.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
