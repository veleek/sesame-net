# Sesame.NET

A .NET client for the [Sesame API](https://docs.candyhouse.co/).  Sesame.NET is licensed under the [MIT license](./LICENSE).

## Installation

You can either download the <strike>[Sesame.NET NuGet package](https://www.nuget.org/packages/Ben.Sesame)</strike> (not available yet) or download the binaries [directly from GitHub](https://github.com/veleek/sesame-net/releases/latest).

## Usage

1. Initialize a new `SesameClient`.

        SesameClient client = new SesameClient();

2. Login with your Sesame username and password.

        client.Login("user@email.com", "super-password");

3. Make other calls to list or control sesames.

    ```csharp
    // Get ALL Sesames associated with the account.
    List<SesameInfo> sesames = await client.ListSesamesAsync();

    // Get a specific Sesame if you already have the ID.
    SesameInfo sesame = await client.GetSesameAsync("ABCD12345678");

    // Execute operations.
    await client.ControlSesameAsync(sesames, ControlType.Lock);
    ```

## Building

Sesame.NET is built using .NET Core against .NET Standard 1.4.

1. [Install .NET Core](https://www.microsoft.com/net/core)
2. Clone the Sesame.NET repository
3. Restore all of the NuGet packages for the project and build it

        dotnet restore
        dotnet build

4. *(Optional)* You can generate a NuGet package using the pack command
        
        dotnet pack Sesame
        # To build release package
        #   dotnet pack Sesame -c Release