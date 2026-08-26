// <copyright file="ProductTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Tests
{
    using AutomationExercise.Tests.Fixtures;
    using AutomationExercise.Tests.Ui.Pages;
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

            Assert.That(
                productsResponse.ResponseCode,
                Is.EqualTo(200));

            Assert.That(
                productsResponse.Products,
                Is.Not.Empty);

            var expectedProduct =
                productsResponse.Products.First();

            var homePage = new HomePage(this.Page);

            await homePage.OpenAsync();

            var productsPage =
                await homePage.GoToProductsAsync();

            await productsPage.SearchAsync(
                expectedProduct.Name);

            var productIsVisible =
                await productsPage.IsProductVisibleAsync(
                    expectedProduct.Name);

            Assert.That(
                productIsVisible,
                Is.True);

            var productDetailsPage =
                await productsPage.OpenProductAsync(
                    expectedProduct.Name);

            var actualName =
                await productDetailsPage.GetNameAsync();

            var actualPrice =
                await productDetailsPage.GetPriceAsync();

            Assert.Multiple(() =>
            {
                Assert.That(
                    actualName,
                    Is.EqualTo(expectedProduct.Name));

                Assert.That(
                    actualPrice,
                    Is.EqualTo(expectedProduct.Price));
            });
        }
    }
}