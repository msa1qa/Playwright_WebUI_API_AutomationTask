// <copyright file="Validator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace AutomationExercise.Tests.Validators
{
    using AutomationExercise.Tests.Api.Constants;
    using AutomationExercise.Tests.Api.Models;

    /// <summary>
    /// Provides reusable validation methods for test scenarios.
    /// </summary>
    public static class Validator
    { /// <summary>
      /// Validates a successful account creation response.
      /// </summary>
      /// <param name="response">The account creation API response.</param>
        public static void ValidateAccountCreation(ApiResponse response)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    response.ResponseCode,
                    Is.EqualTo((int)ApiResponseCode.Created),
                    "Account creation should return the expected Created response code.");

                Assert.That(
                    response.Message,
                    Is.EqualTo(ApiResponseMessages.UserCreated),
                    "Account creation should return the expected success message.");
            }
        }

        /// <summary>
        /// Validates a successful login verification response.
        /// </summary>
        /// <param name="response">The login verification API response.</param>
        public static void ValidateLoginVerification(ApiResponse response)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    response.ResponseCode,
                    Is.EqualTo((int)ApiResponseCode.Ok),
                    "Login verification should return the expected OK response code.");

                Assert.That(
                    response.Message,
                    Is.EqualTo(ApiResponseMessages.UserExists),
                    "Login verification should confirm that the user exists.");
            }
        }

        /// <summary>
        /// Validates that the products API returned products successfully.
        /// </summary>
        /// <param name="response">The products API response.</param>
        public static void ValidateProductsResponse(ProductsResponse response)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    response.ResponseCode,
                    Is.EqualTo((int)ApiResponseCode.Ok),
                    "Products API should return the expected OK response code.");

                Assert.That(
                    response.Products,
                    Is.Not.Empty,
                    "Products API should return at least one product.");
            }
        }

        /// <summary>
        /// Validates product details returned by the UI against the API product.
        /// </summary>
        /// <param name="expectedProduct">The expected API product.</param>
        /// <param name="actualName">The product name displayed in the UI.</param>
        /// <param name="actualPrice">The product price displayed in the UI.</param>
        public static void ValidateProductDetails(
            Product expectedProduct,
            string actualName,
            string actualPrice)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    actualName,
                    Is.EqualTo(expectedProduct.Name),
                    "The UI product name should match the product name returned by the API.");

                Assert.That(
                    actualPrice,
                    Is.EqualTo(expectedProduct.Price),
                    "The UI product price should match the product price returned by the API.");
            }
        }
    }
}
