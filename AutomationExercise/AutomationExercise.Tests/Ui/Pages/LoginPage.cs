// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Ui.Pages
{
    using Microsoft.Playwright;

    /// <summary>
    /// Represents the Automation Exercise login page.
    /// </summary>
    public sealed class LoginPage
    {
        private readonly IPage page;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page.</param>
        public LoginPage(IPage page)
        {
            this.page = page;
        }

        private ILocator EmailInput =>
            this.page.Locator("[data-qa='login-email']");

        private ILocator PasswordInput =>
            this.page.Locator("[data-qa='login-password']");

        private ILocator LoginButton =>
            this.page.Locator("[data-qa='login-button']");

        /// <summary>
        /// Logs in using the specified credentials.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoginAsync(
            string email,
            string password)
        {
            await this.EmailInput.FillAsync(email);
            await this.PasswordInput.FillAsync(password);
            await this.LoginButton.ClickAsync();
        }
    }
}
