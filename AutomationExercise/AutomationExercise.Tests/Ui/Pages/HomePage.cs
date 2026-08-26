// <copyright file="HomePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Ui.Pages
{
    using Microsoft.Playwright;

    /// <summary>
    /// Represents the Automation Exercise home page.
    /// </summary>
    public sealed class HomePage
    {
        private readonly IPage page;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page.</param>
        public HomePage(IPage page)
        {
            this.page = page;
        }

        private ILocator SignupLoginLink =>
            this.page.GetByRole(
                AriaRole.Link,
                new() { Name = "Signup / Login" });

        private ILocator ProductsLink =>
            this.page.GetByRole(
                AriaRole.Link,
                new() { Name = "Products" });

        /// <summary>
        /// Opens the aplication home page.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenAsync()
        {
            await this.page.GotoAsync("/");

            await this.CloseAdIfPresentAsync();
        }

        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        /// <returns>The login page object.</returns>
        public async Task<LoginPage> GoToLoginAsync()
        {
            await this.SignupLoginLink.ClickAsync();
            return new LoginPage(this.page);
        }

        /// <summary>
        /// Navigates to the products page.
        /// </summary>
        /// <returns>The products page object.</returns>
        public async Task<ProductsPage> GoToProductsAsync()
        {
            await this.ProductsLink.ClickAsync();

            await this.CloseAdIfPresentAsync();

            await this.page.WaitForURLAsync("**/products");

            return new ProductsPage(this.page);
        }

        /// <summary>
        /// Gets the logged-in username displayed in the header.
        /// </summary>
        /// <returns>The displayed username.</returns>
        public async Task<string> GetLoggedInUsernameAsync()
        {
            var loggedInText = await this.page
                .GetByText("Logged in as")
                .InnerTextAsync();

            return loggedInText
                .Replace("Logged in as", string.Empty)
                .Trim();
        }

        private async Task CloseAdIfPresentAsync()
        {
            if (!this.page.Url.Contains(
                "#google_vignette",
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            foreach (var frame in this.page.Frames)
            {
                var closeButton = frame.GetByRole(
                    AriaRole.Button,
                    new() { Name = "Close ad" });

                if (await closeButton.IsVisibleAsync())
                {
                    await closeButton.ClickAsync();
                    return;
                }
            }
        }
    }
}
