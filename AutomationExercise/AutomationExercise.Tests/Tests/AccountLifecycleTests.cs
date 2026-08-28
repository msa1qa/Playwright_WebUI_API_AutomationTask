// <copyright file="AccountLifecycleTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Tests
{
    using AutomationExercise.Tests.Api.Constants;
    using AutomationExercise.Tests.Api.Models;
    using AutomationExercise.Tests.Fixtures;
    using AutomationExercise.Tests.TestData;
    using AutomationExercise.Tests.Ui.Pages;
    using AutomationExercise.Tests.Validators;
    using NUnit.Framework;

    /// <summary>
    /// Contains account lifecycle tests.
    /// </summary>
    [TestFixture]
    public sealed class AccountLifecycleTests : TestBase
    {/// <summary>
     /// Verifies that an account created through the API
     /// can be authenticated and recognized through the UI.
     /// </summary>
     /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task AccountCreatedThroughApiCanBeVerifiedThroughUi()
        {
            var account = TestDataFactory.CreateAccount();

            try
            {
                var createResponse =
                    await this.ApiClient.CreateAccountAsync(account);

                account.IsCreated =
                    createResponse.ResponseCode == (int)ApiResponseCode.Created;

                Validator.ValidateAccountCreation(createResponse);

                var verifyResponse =
                    await this.ApiClient.VerifyLoginAsync(
                        account.Email,
                        account.Password);

                Validator.ValidateLoginVerification(verifyResponse);

                var homePage = new HomePage(this.Page);

                await homePage.OpenAsync();

                var loginPage =
                    await homePage.GoToLoginAsync();

                await loginPage.LoginAsync(
                    account.Email,
                    account.Password);

                await homePage.ExpectLoggedInUsernameAsync(
                    account.Name);
            }
            finally
            {
                if (account.IsCreated)
                {
                    await this.ApiClient.DeleteAccountAsync(
                        account.Email,
                        account.Password);
                }
            }
        }
    }
}