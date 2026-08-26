// <copyright file="AccountLifecycleTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomationExercise.Tests.Tests
{
    using AutomationExercise.Tests.Fixtures;
    using AutomationExercise.Tests.TestData;
    using AutomationExercise.Tests.Ui.Pages;
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
            var accountCreated = false;

            try
            {
                var createResponse =
                    await this.ApiClient.CreateAccountAsync(account);

                accountCreated =
                    createResponse.ResponseCode == 201;

                Assert.Multiple(() =>
                {
                    Assert.That(
                        createResponse.ResponseCode,
                        Is.EqualTo(201));

                    Assert.That(
                        createResponse.Message,
                        Is.EqualTo("User created!"));
                });

                var verifyResponse =
                    await this.ApiClient.VerifyLoginAsync(
                        account.Email,
                        account.Password);

                Assert.Multiple(() =>
                {
                    Assert.That(
                        verifyResponse.ResponseCode,
                        Is.EqualTo(200));

                    Assert.That(
                        verifyResponse.Message,
                        Is.EqualTo("User exists!"));
                });

                var homePage = new HomePage(this.Page);

                await homePage.OpenAsync();

                var loginPage =
                    await homePage.GoToLoginAsync();

                await loginPage.LoginAsync(
                    account.Email,
                    account.Password);

                var displayedUsername =
                    await homePage.GetLoggedInUsernameAsync();

                Assert.That(
                    displayedUsername,
                    Is.EqualTo(account.Name));
            }
            finally
            {
                if (accountCreated)
                {
                    await this.ApiClient.DeleteAccountAsync(
                        account.Email,
                        account.Password);
                }
            }
        }
    }
}