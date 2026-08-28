// <copyright file="ProductTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Tests
{
    using AutomationExercise.Tests.Api.Constants;
    using AutomationExercise.Tests.Fixtures;
    using AutomationExercise.Tests.Ui.Pages;
    using AutomationExercise.Tests.Validators;
    using NUnit.Framework;

    /// <summary>
    /// Contains product API-to-UI verification tests.
    /// </summary>
    [TestFixture]
    public sealed class ProductTests : TestBase
    {
        /// <summary>
        /// Verifies that product data returned by the API
        /// matches the product information displayed in the UI.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task ProductReturnedByApiMatchesUiProductDetails()
        {
            var productsResponse =
                await this.ApiClient.GetProductsAsync();

            Validator.ValidateProductsResponse(productsResponse);

            var expectedProduct =
                productsResponse.Products.First();

            var homePage = new HomePage(this.Page);

            await homePage.OpenAsync();

            var productsPage =
                await homePage.GoToProductsAsync();

            await productsPage.SearchAsync(
                expectedProduct.Name);

            await productsPage.ExpectProductVisibleAsync(
                expectedProduct.Name);

            var productDetailsPage =
                await productsPage.OpenProductAsync(
                    expectedProduct.Name);

            var actualName =
                await productDetailsPage.GetNameAsync();

            var actualPrice =
                await productDetailsPage.GetPriceAsync();

            Validator.ValidateProductDetails(
                expectedProduct,
                actualName,
                actualPrice);
        }
    }
}