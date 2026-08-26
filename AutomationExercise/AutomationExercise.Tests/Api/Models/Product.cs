// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Api.Models
{
    /// <summary>
    /// Represent the data of the Products API.
    /// </summary>
    public sealed record Product
    {
        /// <summary>
        /// Gets the product's id.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets the product's Name.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets the product's price.
        /// </summary>
        public string Price { get; init; } = string.Empty;

        /// <summary>
        /// Gets the product's brand.
        /// </summary>
        public string Brand { get; init; } = string.Empty;
    }
}