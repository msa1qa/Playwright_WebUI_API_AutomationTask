// <copyright file="AutomationExerciseApiClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationExercise.Tests.Api.Clients
{
    using AutomationExercise.Tests.Api.Models;
    using Microsoft.Playwright;

    /// <summary>
    /// Provides API operations for the Automation Exercise application.
    /// </summary>
    public sealed class AutomationExerciseApiClient : ApiClientBase
    {
        private readonly IAPIRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AutomationExerciseApiClient"/> class.
        /// </summary>
        /// <param name="requestContext">
        /// The Playwright API request context.
        /// </param>
        public AutomationExerciseApiClient(
            IAPIRequestContext requestContext)
            : base(requestContext)
        {
            this.requestContext = requestContext;
        }

        /// <summary>
        /// Creates a user account.
        /// </summary>
        /// <param name="account">
        /// The account information used to create the user.
        /// </param>
        /// <returns>The API response.</returns>
        public async Task<ApiResponse> CreateAccountAsync(
            CreateAccountRequest account)
        {
            var form = this.CreateAccountForm(account);

            return await this.PostAsync<ApiResponse>(
                "/api/createAccount",
                form);
        }

        /// <summary>
        /// Verifies that a user exists using login credentials.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The API response.</returns>
        public Task<ApiResponse> VerifyLoginAsync(
            string email,
            string password)
        {
            var form = this.CreateCredentialsForm(
                email,
                password);

            return this.PostAsync<ApiResponse>(
                "/api/verifyLogin",
                form);
        }

        /// <summary>
        /// Deletes a user account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The API response.</returns>
        public Task<ApiResponse> DeleteAccountAsync(
            string email,
            string password)
        {
            var form = this.CreateCredentialsForm(
                email,
                password);

            return this.DeleteAsync<ApiResponse>(
                "/api/deleteAccount",
                form);
        }

        /// <summary>
        /// Gets the list of products.
        /// </summary>
        /// <returns>The products API response.</returns>
        public Task<ProductsResponse> GetProductsAsync()
        {
            return this.GetAsync<ProductsResponse>(
                "/api/productsList");
        }

        private IFormData CreateAccountForm(CreateAccountRequest account)
        {
            var form = this.requestContext.CreateFormData();

            form.Set("name", account.Name);
            form.Set("email", account.Email);
            form.Set("password", account.Password);
            form.Set("title", account.Title);
            form.Set("birth_date", account.BirthDate);
            form.Set("birth_month", account.BirthMonth);
            form.Set("birth_year", account.BirthYear);
            form.Set("firstname", account.FirstName);
            form.Set("lastname", account.LastName);
            form.Set("company", account.Company);
            form.Set("address1", account.Address1);
            form.Set("address2", account.Address2);
            form.Set("country", account.Country);
            form.Set("zipcode", account.ZipCode);
            form.Set("state", account.State);
            form.Set("city", account.City);
            form.Set("mobile_number", account.MobileNumber);

            return form;
        }

        private IFormData CreateCredentialsForm(
            string email,
            string password)
        {
            var form = this.requestContext.CreateFormData();

            form.Set("email", email);
            form.Set("password", password);

            return form;
        }
    }
}
