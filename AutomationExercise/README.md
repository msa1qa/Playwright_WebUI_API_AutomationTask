# Automation Exercise - Web UI & API Test Automation

Automation framework for the Automation Exercise application.

The project demonstrates cross-layer test automation using API calls for fast test setup/data retrieval and Playwright UI automation for independent user-facing verification.

## Technology Stack

- C#
- .NET 8
- Microsoft Playwright for .NET
- NUnit
- Microsoft.Playwright.NUnit
- StyleCop Analyzers
- .NET/Roslyn Analyzers
- GitHub Actions

## Project Structure

```text
AutomationExercise/
├── AutomationExercise.Tests/
│   ├── Api/
│   │   ├── Clients/
│   │   │   ├── ApiClientBase.cs
│   │   │   └── AutomationExerciseApiClient.cs
│   │   └── Models/
│   │       ├── ApiResponse.cs
│   │       ├── CreateAccountRequest.cs
│   │       ├── Product.cs
│   │       └── ProductsResponse.cs
│   ├── Configuration/
│   │   └── TestSettings.cs
│   ├── Fixtures/
│   │   └── TestBase.cs
│   ├── TestData/
│   │   └── TestDataFactory.cs
│   ├── Tests/
│   │   ├── AccountLifecycleTests.cs
│   │   └── ProductTests.cs
│   └── Ui/
│       └── Pages/
│           ├── HomePage.cs
│           ├── LoginPage.cs
│           ├── ProductsPage.cs
│           └── ProductDetailsPage.cs
├── Directory.Build.props
├── NOTES.md
└── README.md
```

## Test Scenarios

### Test 1 - Account Lifecycle: API Creates, UI Verifies

1. Generate unique account data.
2. Create the account using `POST /api/createAccount`.
3. Verify the account using `POST /api/verifyLogin`.
4. Open the application through Playwright.
5. Log in through the UI using the same API-created credentials.
6. Verify the API-created account name against the authenticated UI.
7. Delete the account through `DELETE /api/deleteAccount` during cleanup.

Account deletion is executed in a `finally` block so cleanup is attempted even if the test fails.

### Test 2 - Product Data: API Reads, UI Verifies

1. Retrieve products using `GET /api/productsList`.
2. Select the first returned product as a deterministic test target.
3. Capture the product name and price from the API response.
4. Open the Products page through the UI.
5. Search using the exact API-provided product name.
6. Verify that the product appears.
7. Open the product details page.
8. Compare the displayed product name and price against the API values.

This test is read-only and does not modify product data.

## Prerequisites

- .NET 8 SDK
- Git
- PowerShell
- A supported Playwright browser

Verify .NET:

```powershell
dotnet --version
```

## Setup

```powershell
dotnet restore
dotnet build
```

After the initial build, install Chromium:

```powershell
pwsh AutomationExercise.Tests/bin/Debug/net8.0/playwright.ps1 install chromium
```

## Running Tests

Run all tests:

```powershell
dotnet test
```

Run Test 1:

```powershell
dotnet test --filter AccountLifecycleTests
```

Run Test 2:

```powershell
dotnet test --filter ProductTests
```

## Headed Execution

Tests run headless by default. To watch the browser locally:

```powershell
$env:HEADED="1"
dotnet test
```

Remove the variable afterward:

```powershell
Remove-Item Env:HEADED -ErrorAction SilentlyContinue
```

## Playwright Debug Mode

```powershell
$env:PWDEBUG="1"
dotnet test --filter AccountLifecycleTests
```

Remove the variable afterward:

```powershell
Remove-Item Env:PWDEBUG -ErrorAction SilentlyContinue
```

A local `local.runsettings` file can also be used for Visual Studio Test Explorer debugging. It is excluded from source control so local debugging preferences do not affect CI.

## Code Quality

The project uses StyleCop and .NET/Roslyn analyzers. Warnings are treated as errors through `Directory.Build.props`.

```powershell
dotnet format
dotnet format --verify-no-changes
dotnet build --configuration Release
```

## Final Local Validation

```powershell
dotnet format --verify-no-changes
dotnet build --configuration Release
dotnet test --configuration Release
```

Expected result:

```text
0 warnings
0 errors
2 tests passed
```

## CI

GitHub Actions runs the quality gates and test suite in headless mode. The CI pipeline restores dependencies, verifies formatting, builds the solution, installs the required Playwright browser, and executes the NUnit tests.
