# .NET Core Console App with DI Logging Sample Remix

* Super simple .NET Console app using DI, Serilog and Json Settings
* Without using generic host even though many samples are using it


## Setup

```bash
cd CoreConsoleApp
dotnet new sln -n BetterConsoleApp
dotnet new console -n ConsoleUI
dotnet sln BetterConsoleApp.sln add **/*.csproj

cd ConsoleUI
dotnet add package Microsoft.CodeAnalysis.FxCopAnalyzers
# NOTE: no need to add Hosting
# dotnet add package Microsoft.Extensions.Hosting
# dotnet add package Serilog.Extensions.Hosting
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Serilog.Extensions.Logging
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console

touch appsettings.json
# add CopyToOutputDirectory Always in .csproj
```

## Run

```bash
cd ConsoleUI
dotnet restore
dotnet build
dotnet run
```

__Output__

```
[15:00:01 INF] Application Starting
[15:00:01 INF] Greet number 0
[15:00:01 INF] Greet number 1
[15:00:01 INF] Greet number 2
[15:00:01 INF] Greet number 3
[15:00:01 INF] Greet number 4
```

## Reference

* .NET Core Console App with Dependency Injection, Logging, and Settings
  - NOTE: this video is using generic host but it is not necessary
    - generic host is to create server/service program running in background
    - i.e. normal batch will not need generic host
  - https://www.youtube.com/watch?v=GAOCe-2nXqc
* .Net Core の Generic Host とは何か
  - https://qiita.com/kitauji/items/a7c6a7d33c9f67c79336
* Microsoft.Extensions.DependencyInjection を使った DI の基本
  - https://qiita.com/TsuyoshiUshio@github/items/20412b36fe63f05671c9
