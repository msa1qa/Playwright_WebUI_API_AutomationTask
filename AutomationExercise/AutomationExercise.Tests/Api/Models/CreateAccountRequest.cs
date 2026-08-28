// <copyright file="CreateAccountRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Api.Models
{
    /// <summary>
    /// Represents the data required to create a user account.
    /// </summary>
    public sealed record CreateAccountRequest
    {
        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// Gets the user's password.
        /// </summary>
        public required string Password { get; init; }

        /// <summary>
        /// Gets the user's title.
        /// </summary>
        public string Title { get; init; } = "Mr";

        /// <summary>
        /// Gets the user's birth day.
        /// </summary>
        public string BirthDate { get; init; } = "10";

        /// <summary>
        /// Gets the user's birth month.
        /// </summary>
        public string BirthMonth { get; init; } = "5";

        /// <summary>
        /// Gets the user's birth year.
        /// </summary>
        public string BirthYear { get; init; } = "1990";

        /// <summary>
        /// Gets the user's first name.
        /// </summary>
        public string FirstName { get; init; } = "Automation";

        /// <summary>
        /// Gets the user's last name.
        /// </summary>
        public string LastName { get; init; } = "Tester";

        /// <summary>
        /// Gets the user's company.
        /// </summary>
        public string Company { get; init; } = "QA";

        /// <summary>
        /// Gets the first line of the user's address.
        /// </summary>
        public string Address1 { get; init; } = "1 Automation Street";

        /// <summary>
        /// Gets the second line of the user's address.
        /// </summary>
        public string Address2 { get; init; } = "Suite 100";

        /// <summary>
        /// Gets the user's country.
        /// </summary>
        public string Country { get; init; } = "United States";

        /// <summary>
        /// Gets the user's ZIP code.
        /// </summary>
        public string ZipCode { get; init; } = "92101";

        /// <summary>
        /// Gets the user's state.
        /// </summary>
        public string State { get; init; } = "California";

        /// <summary>
        /// Gets the user's city.
        /// </summary>
        public string City { get; init; } = "San Diego";

        /// <summary>
        /// Gets the user's mobile phone number.
        /// </summary>
        public string MobileNumber { get; init; } = "5551234567";

        /// <summary>
        /// Gets or sets a value indicating whether the account was successfully created.
        /// </summary>
        public bool IsCreated { get; set; }
    }
}