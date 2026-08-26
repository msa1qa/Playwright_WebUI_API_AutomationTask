// <copyright file="TestBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Fixtures
{
    using AutomationExercise.Tests.Api.Clients;
    using AutomationExercise.Tests.Configuration;
    using Microsoft.Playwright;
    using Microsoft.Playwright.NUnit;
    using NUnit.Framework;

    /// <summary>
    /// Provides common Playwright UI and API setup for tests.
    /// </summary>
    public class TestBase : PageTest
    {
        private IAPIRequestContext? apiRequestContext;

        /// <summary>
        /// Gets the Automation Exercise API client.
        /// </summary>
        protected AutomationExerciseApiClient ApiClient { get; private set; } = null!;

        /// <summary>
        /// Configures the browser context used by each test.
        /// </summary>
        /// <returns>The browser context options.</returns>
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                BaseURL = TestSettings.BaseUrl,
            };
        }

        /// <summary>
        /// Creates the API request context before each test.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [SetUp]
        public async Task SetUpApiAsync()
        {
            this.apiRequestContext =
                await this.Playwright.APIRequest.NewContextAsync(
                    new APIRequestNewContextOptions
                    {
                        BaseURL = TestSettings.BaseUrl,
                    });

            this.ApiClient =
                new AutomationExerciseApiClient(
                    this.apiRequestContext);
        }

        /// <summary>
        /// Disposes the API request context after each test.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [TearDown]
        public async Task TearDownApiAsync()
        {
            if (this.apiRequestContext is not null)
            {
                await this.apiRequestContext.DisposeAsync();
            }
        }
    }
}
