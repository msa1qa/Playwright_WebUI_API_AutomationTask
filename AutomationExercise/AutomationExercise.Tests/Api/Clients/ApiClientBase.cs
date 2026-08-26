// <copyright file="ApiClientBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationExercise.Tests.Api.Clients
{
    using System.Text.Json;
    using Microsoft.Playwright;

    /// <summary>
    /// Provides common functionality for API clients.
    /// </summary>
    public abstract class ApiClientBase
    {
        /// <summary>
        ///  The API request context used to send HTTP requests.
        /// </summary>
        private readonly IAPIRequestContext request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientBase"/> class.
        /// </summary>
        /// <param name="request">The API request context.</param>
        protected ApiClientBase(IAPIRequestContext request)
        {
            this.request = request;
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="endpoint">The API endpoint.</param>
        /// <returns>The API response.</returns>
        protected async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await this.request.GetAsync(endpoint);
            return await DeserializeAsync<T>(response);
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="form">The request options.</param>
        /// <returns>The API response.</returns>
        protected async Task<T> PostAsync<T>(
            string endpoint,
            IFormData form)
        {
            var response = await this.request.FetchAsync(
                endpoint,
                new APIRequestContextOptions
                {
                    Method = "POST",
                    Form = form,
                });
            return await DeserializeAsync<T>(response);
        }

        /// <summary>
        /// Sends a PUT request to the specified endpoint.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="options">The request options.</param>
        /// <returns>The API response.</returns>
        protected Task<IAPIResponse> PutAsync(
            string endpoint,
            APIRequestContextOptions options)
        {
            return this.request.FetchAsync(
                endpoint,
                new APIRequestContextOptions
                {
                    Method = "PUT",
                    Form = options.Form,
                });
        }

        /// <summary>
        /// Sends a DELETE request to the specified endpoint.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="form">The request options.</param>
        /// <returns>The API response.</returns>
        protected async Task<T> DeleteAsync<T>(
            string endpoint,
            IFormData form)
        {
            var response = await this.request.FetchAsync(
                endpoint,
                new APIRequestContextOptions
                {
                    Method = "DELETE",
                    Form = form,
                });
            return await DeserializeAsync<T>(response);
        }

        /// <summary>
        /// Deserializes an API response into the specified type.
        /// </summary>
        /// <typeparam name="T">The target response type.</typeparam>
        /// <param name="response">The API response.</param>
        /// <returns>The deserialized response object.</returns>
        private static async Task<T> DeserializeAsync<T>(
            IAPIResponse response)
        {
            var responseBody = await response.TextAsync();

            var result = JsonSerializer.Deserialize<T>(
                responseBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            if (result is null)
            {
                throw new InvalidOperationException(
                    $"Unable to deserialize API response to {typeof(T).Name}.");
            }

            return result;
        }
    }
}