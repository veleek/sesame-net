# Sesame.NET

A .NET client for the [Sesame API](https://docs.candyhouse.co/).  Sesame.NET is licensed under the [MIT license](./LICENSE).

## Installation

You can either download the [Sesame.NET NuGet package](https://www.nuget.org/packages/Sesame) or download the binaries [directly from GitHub](https://github.com/veleek/sesame-net/releases/latest).

## Usage

1. Configure a Sesame API Key on [the CandyHouse Dashboard](https://my.candyhouse.co/#/credentials).
2. Initialize an instance of `SesameClient`.

    ```csharp
    string apiKey = "<Sesame API Key>";
    SesameClient client = new SesameClient(apiKey);
    ```

3. Make other calls to list or control sesames.

    ```csharp
    // Get ALL Sesames associated with the account.
    List<Sesames> sesames = await client.ListSesamesAsync();

    // Get a specific sesame and refresh it's state.
    Sesame sesame = sesames.FirstOrDefault(s => s.NickName == "Home");
    await sesame.RefreshAsync();

    // Execute operations.
    await sesame.LockAsync();
    await sesame.UnlockAsync();
    ```

## Building

Sesame.NET is built using .NET Core (.NET Standard 1.4 and 2.0).

1. [Install .NET Core](https://www.microsoft.com/net/core)
2. Clone the Sesame.NET repository
3. Restore all of the NuGet packages for the project and build it

        dotnet restore
        dotnet build

4. *(Optional)* You can generate a NuGet package using the pack command
        
        dotnet pack Sesame
        # To build release package
        #   dotnet pack Sesame -c Release
