// <copyright file="ProductsResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Api.Models
{
    /// <summary>
    /// Represents the data returned by the products API.
    /// </summary>
    public sealed record ProductsResponse
    {
        /// <summary>
        /// Gets the API Response Code.
        /// </summary>
        public int ResponseCode { get; init; }

        /// <summary>
        /// Gets the producs list.
        /// </summary>
        public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();
    }
}