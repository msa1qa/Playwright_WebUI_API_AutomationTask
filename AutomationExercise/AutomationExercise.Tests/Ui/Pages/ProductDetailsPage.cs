// <copyright file="ProductDetailsPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Ui.Pages
{
    using Microsoft.Playwright;

    /// <summary>
    /// Represents the Automation Exercise product details page.
    /// </summary>
    public sealed class ProductDetailsPage
    {
        private readonly IPage page;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductDetailsPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page.</param>
        public ProductDetailsPage(IPage page)
        {
            this.page = page;
        }

        private ILocator ProductName =>
            this.page.Locator(".product-information h2");

        private ILocator ProductPrice =>
            this.page.Locator(".product-information span span");

        /// <summary>
        /// Gets the displayed product name.
        /// </summary>
        /// <returns>The displayed product name.</returns>
        public async Task<string> GetNameAsync()
        {
            return (await this.ProductName.InnerTextAsync()).Trim();
        }

        /// <summary>
        /// Gets the displayed product price.
        /// </summary>
        /// <returns>The displayed product price.</returns>
        public async Task<string> GetPriceAsync()
        {
            return (await this.ProductPrice.InnerTextAsync()).Trim();
        }
    }
}
