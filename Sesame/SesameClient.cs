using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ben.CandyHouse
{
    /// <summary>
    /// A client library for interacting with the Sesame API.
    /// </summary>
    public class SesameClient : HttpClient
    {
        private const string ApiBaseAddress = "https://api.candyhouse.co";
        private const string ApiVersion = "v1";
        private const string AuthorizationHeaderName = "X-Authorization";
        
        /// <summary>
        /// Initialize a new <see cref="SesameClient"/>
        /// </summary>
        public SesameClient() : base()
        {
            this.BaseAddress = new Uri(ApiBaseAddress);
        }

        /// <summary>
        /// Gets or sets the Authorization header to use for requests.
        /// </summary>
        public string Authorization
        {
            get
            {
                if (this.DefaultRequestHeaders.TryGetValues("X-Authorization", out IEnumerable<string> values))
                {
                    return values?.FirstOrDefault();
                }

                return null;
            }

            set
            {
                this.DefaultRequestHeaders.Remove(AuthorizationHeaderName);

                if (value != null)
                {
                    this.DefaultRequestHeaders.Add(AuthorizationHeaderName, value);
                }
            }
        }

        /// <summary>
        /// Authenticate with the Sesame service.
        /// </summary>
        /// <param name="email">The email address of the account to authenticate with.</param>
        /// <param name="password">The password of the account to authenticate with.</param>
        public async Task LoginAsync(string email, string password)
        {
            // Clear out the authorization header before performing a login request
            this.Authorization = null;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{ApiVersion}/accounts/login");

            LoginRequest body = new LoginRequest
            {
                Email = email,
                Password = password,
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            
            HttpResponseMessage response = await this.SendAsync(request);
            if(!response.IsSuccessStatusCode)
            {
                SesameError error = await response.Content.ReadAsJsonAsync<SesameError>();
                throw new SesameException(error);
            }

            LoginResponse responseBody = await response.Content.ReadAsJsonAsync<LoginResponse>();
            this.Authorization = responseBody.Authorization;
        }

        /// <summary>
        /// Get the detailed device information for all of the Sesames the logged in account has access to.
        /// </summary>
        /// <returns>A list of device details for the authorized Sesames.</returns>
        public async Task<List<Sesame>> ListSesamesAsync()
        {
            this.EnsureLoggedIn();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/sesames");
            HttpResponseMessage response = await this.SendAsync(request);

            ListSesamesResponse listSesameResponse = await response.Content.ReadAsJsonAsync<ListSesamesResponse>();

            foreach (Sesame sesame in listSesameResponse.Sesames)
            {
                sesame.Client = this;
            }

            return listSesameResponse.Sesames;
        }

        /// <summary>
        /// Gets the device information for a Sesame.
        /// </summary>
        /// <param name="deviceId">The device ID of the Sesame to get details for.</param>
        /// <returns>The complete device detail for the Sesame.</returns>
        public async Task<Sesame> GetSesameAsync(string deviceId)
        {
            this.EnsureLoggedIn();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/sesames/{deviceId}");
            HttpResponseMessage response = await this.SendAsync(request);

            Sesame sesame = await response.Content.ReadAsJsonAsync<Sesame>();
            sesame.Client = this;
            // Currently DeviceId is not returned by this call so we need to manually set it if we want to use it.
            sesame.DeviceId = deviceId;
            return sesame;
        }
        
        /// <summary>
        /// Refresh the device details for a Sesame.
        /// </summary>
        /// <param name="sesame">The sesame to refresh.</param>
        /// <returns>A task that indicates when the refresh operation is complete.</returns>
        public async Task RefreshSesameAsync(Sesame sesame)
        {
            Sesame updated = await this.GetSesameAsync(sesame.DeviceId);
            sesame.ApiEnabled = updated.ApiEnabled;
            sesame.Battery = updated.Battery;
            sesame.IsUnlocked = updated.IsUnlocked;
        }

        /// <summary>
        /// Send a control request to a Sesame to execute an operation.
        /// </summary>
        /// <param name="sesame">The Sesame to control.</param>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>A task that indicates when the control operation is complete.</returns>
        public Task ControlSesameAsync(Sesame sesame, ControlOperation operation)
        {
            return this.ControlSesameAsync(sesame.DeviceId, operation.ToString().ToLower());
        }

        /// <summary>
        /// Send a control request to a Sesame to execute an operation.
        /// </summary>
        /// <param name="sesameId">The ID of the Sesame to control.</param>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>A task that indicates when the control operation is complete.</returns>
        public async Task ControlSesameAsync(string sesameId, string operation)
        {
            this.EnsureLoggedIn();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/sesames/{sesameId}/control");

            ControlRequest body = new ControlRequest
            {
                Type = operation,
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Ensures that a user has been authenticated before attempting to call any APIs that require authentication.
        /// </summary>
        private void EnsureLoggedIn()
        {
            if(string.IsNullOrEmpty(this.Authorization))
            {
                throw new SesameException("You must login before calling this method.");
            }
        }
    }
}
