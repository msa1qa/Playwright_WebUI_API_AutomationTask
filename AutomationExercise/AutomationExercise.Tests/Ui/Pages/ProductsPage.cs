// <copyright file="ProductsPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Ui.Pages
{
    using Microsoft.Playwright;

    /// <summary>
    /// Represents the Automation Exercise products page.
    /// </summary>
    public sealed class ProductsPage
    {
        private readonly IPage page;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page.</param>
        public ProductsPage(IPage page)
        {
            this.page = page;
        }

        private ILocator SearchInput =>
            this.page.Locator("#search_product");

        private ILocator SearchButton =>
            this.page.Locator("#submit_search");

        /// <summary>
        /// Searches for a product by name.
        /// </summary>
        /// <param name="productName">The product name.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SearchAsync(string productName)
        {
            await this.SearchInput.FillAsync(productName);
            await this.SearchButton.ClickAsync();
        }

        /// <summary>
        /// Determines whether the specified product is visible.
        /// </summary>
        /// <param name="productName">The product name.</param>
        /// <returns>
        /// <see langword="true"/> when the product is visible;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public async Task<bool> IsProductVisibleAsync(
            string productName)
        {
            return await this.page
                .Locator(".productinfo")
                .Filter(new() { HasText = productName })
                .IsVisibleAsync();
        }

        /// <summary>
        /// Opens the details page for the specified product.
        /// </summary>
        /// <param name="productName">The product name.</param>
        /// <returns>The product details page object.</returns>
        public async Task<ProductDetailsPage> OpenProductAsync(
            string productName)
        {
            var productCard = this.page
                .Locator(".product-image-wrapper")
                .Filter(new() { HasText = productName });

            await productCard
                .GetByText("View Product")
                .ClickAsync();

            return new ProductDetailsPage(this.page);
        }
    }
}
