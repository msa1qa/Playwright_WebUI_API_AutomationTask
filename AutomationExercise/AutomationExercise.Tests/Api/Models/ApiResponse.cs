// <copyright file="ApiResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Api.Models
{
    /// <summary>
    /// Represent the data returned by the API.
    /// </summary>
    public sealed record ApiResponse
    {
        /// <summary>
        /// Gets the API ResponseCode.
        /// </summary>
        public int ResponseCode { get; init; }

        /// <summary>
        /// Gets the API response message.
        /// </summary>
        public string Message { get; init; } = string.Empty;
    }
}