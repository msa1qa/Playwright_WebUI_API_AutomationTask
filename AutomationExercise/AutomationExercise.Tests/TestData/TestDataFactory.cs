// <copyright file="TestDataFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.TestData
{
    using AutomationExercise.Tests.Api.Models;

    /// <summary>
    /// Provides test data for automated tests.
    /// </summary>
    public static class TestDataFactory
    {
        /// <summary>
        /// Creates account test data with unique user information.
        /// </summary>
        /// <returns>The account creation request.</returns>
        public static CreateAccountRequest CreateAccount()
        {
            var unique = Guid.NewGuid().ToString("N");

            return new CreateAccountRequest
            {
                Name = $"AutomationUser-{unique[..8]}",
                Email = $"automation0{unique}@example.com",
                Password = $"Pw!{unique[..12]}",
            };
        }
    }
}
