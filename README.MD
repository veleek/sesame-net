# Sesame.NET

A .NET client for the [Sesame API](https://docs.candyhouse.co/).  Sesame.NET is licensed under the [MIT license](./LICENSE).

## Installation

You can either download the [Sesame.NET NuGet package](https://www.nuget.org/packages/Ben.Sesame) or download the binaries [directly from GitHub](./releases/latest).

## Usage

1. Initialize a new `SesameClient`.

        SesameClient client = new SesameClient();

2. Login with your Sesame username and password.

        client.Login("user@email.com", "super-password");

3. Make other calls to list or control sesames.

    ```csharp
    List<SesameInfo> sesames = await client.ListSesamesAsync();
    await client.ControlSesameAsync(sesames[0], ControlType.Lock);
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