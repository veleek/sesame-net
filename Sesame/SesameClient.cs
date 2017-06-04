using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Sesame
{
    public class SesameClient : HttpClient
    {
        public const string ApiBaseAddress = "https://api.candyhouse.co";
        public const string AuthorizationHeaderName = "X-Authorization";

        public SesameClient() : base()
        {
            this.BaseAddress = new Uri(ApiBaseAddress);
        }

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

        public async Task LoginAsync(string email, string password)
        {
            // Clear out the authorization header before performing a login request
            this.Authorization = null;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/accounts/login");

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

        public async Task<List<SesameInfo>> ListSesamesAsync()
        {
            this.EnsureLoggedIn();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "v1/sesames");
            HttpResponseMessage response = await this.SendAsync(request);

            ListSesamesResponse listSesameResponse = await response.Content.ReadAsJsonAsync<ListSesamesResponse>();
            return listSesameResponse.Sesames;
        }

        public Task ControlSesame(SesameInfo sesame, ControlType type)
        {
            if(type == ControlType.Toggle)
            {
                type = sesame.IsUnlocked ? ControlType.Lock : ControlType.Unlock;
            }

            return ControlSesame(sesame.DeviceId, type.ToString().ToLower());
        }

        public async Task ControlSesame(string sesameId, string type)
        {
            this.EnsureLoggedIn();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/sesames/{sesameId}/control");

            ControlRequest body = new ControlRequest
            {
                Type = type,
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private void EnsureLoggedIn()
        {
            if(string.IsNullOrEmpty(this.Authorization))
            {
                throw new SesameException("You must login before calling this method.");
            }
        }
    }

    public class ListSesamesResponse
    {
        [JsonProperty("sesames")]
        public List<SesameInfo> Sesames { get; set; }
    }
}
