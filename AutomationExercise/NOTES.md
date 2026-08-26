# Implementation Notes

## Architecture

The framework separates API operations, UI interactions, test data, configuration, and test orchestration. Tests remain focused on business behavior while reusable technical implementation is encapsulated in framework components.

## API Client Design

`ApiClientBase` provides reusable HTTP functionality. `AutomationExerciseApiClient` contains application-specific operations for account creation, login verification, account deletion, and product retrieval.

Request and response data is represented with strongly typed models. Tests do not perform raw HTTP requests directly.

## API and UI Context Isolation

The Playwright API request context is created separately from the browser context. This prevents API operations from implicitly authenticating the browser through shared cookies.

The account lifecycle test therefore creates and verifies data through the API and independently authenticates through the UI.

## Page Object Model

UI locators and interactions are encapsulated inside Page Objects. Tests do not contain raw Playwright locators.

Assertions remain in the NUnit tests so Page Objects are responsible for interacting with and observing the UI rather than defining test expectations.

## Account Test Data

`TestDataFactory` generates unique account information for every execution, including a unique email address to avoid collisions.

The same generated account data is used for API account creation, API login verification, UI login, UI account-name verification, and API cleanup.

## Account Profile Verification

Automation Exercise does not expose a conventional authenticated account/profile page in its available UI flow.

The account name submitted through `POST /api/createAccount` is therefore verified against the authenticated `Logged in as <name>` value displayed by the UI after login.

This independently confirms that the API-created account is recognized without introducing unrelated checkout/order steps solely to expose additional registration information.

## Account Cleanup

The account lifecycle test modifies application data, so it removes the account it creates. Deletion is performed through the API inside a `finally` block so cleanup is attempted even when an assertion or UI operation fails.

## Product Selection

Test Case 2 selects the first product returned by `GET /api/productsList`. This provides deterministic selection while keeping expected data dynamic.

The product name and price are not hardcoded. Both values come from the API response and are independently verified through the UI.

## Product Test Data Ownership

The product test is read-only. It retrieves existing data through the API and verifies it through the UI without creating, modifying, or deleting product data.

## Third-Party Advertising

Automation Exercise can display Google vignette advertisements during UI navigation. These advertisements are external to the application under test and can intercept navigation.

The Page Object layer contains targeted handling for the advertisement close control when a Google vignette is detected. Google advertising frames can also be dynamically attached and detached, so the handling tolerates that third-party behavior.

## Waiting Strategy

Fixed sleeps such as `Task.Delay` and `WaitForTimeoutAsync` are intentionally avoided. The framework relies on Playwright auto-waiting and explicit state-based waits where navigation or application state must be confirmed.

## Static Analysis

StyleCop and .NET/Roslyn analyzers are enabled. Warnings are treated as build errors to maintain a zero-warning build. Formatting and analyzer compliance are validated locally before submission.

## Local Debugging

Tests run headless by default. Headed execution and Playwright debugging can be enabled locally through environment variables or a `local.runsettings` file.

The local run settings file is excluded from Git so local debugging preferences do not affect CI.

## CI Execution

CI runs tests headlessly and should perform the same primary quality gates used locally:

1. Restore dependencies.
2. Verify formatting.
3. Build the solution.
4. Install the Playwright browser.
5. Execute the automated tests.

## Test Isolation and Parallel Execution

The framework uses unique account data to reduce test-data collisions. The two tests do not depend on each other's state: the account lifecycle test owns and cleans up its generated account, while the product verification test is read-only.

This avoids test ordering dependencies and supports isolated execution.
