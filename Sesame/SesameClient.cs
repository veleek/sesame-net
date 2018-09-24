using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private const string ApiVersion = "public";
        
        /// <summary>
        /// Initialize a new <see cref="SesameClient"/>
        /// </summary>
        public SesameClient(string apiKey)
        {
            this.BaseAddress = new Uri(ApiBaseAddress);

            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey);
        }

        /// <summary>
        /// Get the detailed device information for all of the Sesames the logged in account has access to.
        /// </summary>
        /// <returns>A list of device details for the authorized Sesames.</returns>
        public async Task<List<Sesame>> ListSesamesAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/sesames");

            using (HttpResponseMessage response = await this.SendAsync(request))
            {
                await this.EnsureSuccessStatusCodeAsync(response);

                List<Sesame> sesames = await response.Content.ReadAsJsonAsync<List<Sesame>>();

                foreach (Sesame sesame in sesames)
                {
                    sesame.Client = this;
                }

                return sesames;
            }
        }

        /// <summary>
        /// Gets the current state information for a Sesame.
        /// </summary>
        /// <remarks>
        /// This method only returns the state for the Sesame.  The get detailed device information such as nickname
        /// and Sesame model you must use <see cref="ListSesamesAsync"/> then use <see cref="RefreshSesameAsync"/>
        /// to populate the device state.</remarks>
        /// <param name="deviceId">The device ID of the Sesame to get details for.</param>
        /// <returns>The state information for the Sesame with the given device ID.</returns>
        public async Task<Sesame> GetSesameStateAsync(string deviceId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/sesame/{deviceId}");

            using (HttpResponseMessage response = await this.SendAsync(request))
            {
                await this.EnsureSuccessStatusCodeAsync(response);

                Sesame sesame = await response.Content.ReadAsJsonAsync<Sesame>();
                sesame.Client = this;
                // Currently DeviceId is not returned by this call so we need to manually set it if we want to use it.
                sesame.DeviceId = deviceId;
                return sesame;
            }
        }
        
        /// <summary>
        /// Refresh the device details for a Sesame.
        /// </summary>
        /// <param name="sesame">The sesame to refresh.</param>
        /// <returns>A task that indicates when the refresh operation is complete.</returns>
        public async Task RefreshSesameAsync(Sesame sesame)
        {
            string syncTaskId = await this.ControlSesameAsync(sesame, ControlOperation.Sync);
            await this.WaitForOperationAsync(syncTaskId);

            Sesame updated = await this.GetSesameStateAsync(sesame.DeviceId);
            sesame.IsLocked = updated.IsLocked;
            sesame.Battery = updated.Battery;
            sesame.IsResponsive = updated.IsResponsive;
        }

        /// <summary>
        /// Send a control request to a Sesame to execute an operation.
        /// </summary>
        /// <param name="sesame">The Sesame to control.</param>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>A task that indicates when the control operation is complete.</returns>
        public async Task<string> ControlSesameAsync(Sesame sesame, ControlOperation operation)
        {
            return await this.ControlSesameAsync(sesame.DeviceId, operation.ToString().ToLower());
        }

        /// <summary>
        /// Send a control request to a Sesame to execute an operation.
        /// </summary>
        /// <param name="sesameId">The ID of the Sesame to control.</param>
        /// <param name="operation">The operation to execute.</param>
        /// <returns>A task that indicates when the control operation is complete.</returns>
        public async Task<string> ControlSesameAsync(string sesameId, string operation)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{ApiVersion}/sesame/{sesameId}");

            ControlRequest body = new ControlRequest
            {
                Command = operation,
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await this.SendAsync(request))
            {
                await this.EnsureSuccessStatusCodeAsync(response);

                ControlResponse controlResponse = await response.Content.ReadAsJsonAsync<ControlResponse>();
                return controlResponse.TaskId;
            }
        }

        /// <summary>
        /// Query the result of a previous control request to determine whether it executed or not.
        /// </summary>
        /// <param name="taskId">The ID of the control request to query the result of.</param>
        /// <returns>A <see cref="ExecutionResult"/> indicating the status of the control request.</returns>
        public async Task<ExecutionResult> QueryExecutionResultAsync(string taskId)
        {
            HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/action-result?task_id={taskId}");

            using (HttpResponseMessage response = await this.SendAsync(request))
            {
                await this.EnsureSuccessStatusCodeAsync(response);

                return await response.Content.ReadAsJsonAsync<ExecutionResult>();
            }
        }

        /// <summary>
        /// Wait for an operation to complete (i.e. until it returns a success or failure).
        /// </summary>
        /// <param name="taskId">The ID of the operation to wait for.</param>
        /// <returns>The final execution result of the operation.</returns>
        public async Task<ExecutionResult> WaitForOperationAsync(string taskId)
        {
            int delay = 500;
            ExecutionResult result;
            do
            {
                // Always delay a little bit because otherwise we could get a bad request.
                await Task.Delay(delay);
                delay = 1000;

                result = await this.QueryExecutionResultAsync(taskId);
            }
            while (result.Successful == null);

            return result;
        }

        private async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                SesameError error = await response.Content.ReadAsJsonAsync<SesameError>();
                throw new SesameException(error.Error, response.StatusCode);
            }
        }
    }
}
