# C# Event Demo

* this demo console app shows simple event sample
* using Action/Func as well as EventHandler and EventArgs


## Key Points

* Delegate is a kind of pointer to callback function
* Event is improved version of Delegate for better control

## Setup

```bash
cd ConsoleEventDemo
dotnet new sln -n ConsoleEventDemo
dotnet new console -n EventDemoApp
dotnet sln ConsoleEventDemo.sln add **/*.csproj

cd EventDemoApp
dotnet add package Microsoft.CodeAnalysis.FxCopAnalyzers
```

## Run

```
cd EventDemoApp
dotnet restore
dotnet build
dotnet run
```

## Reference

* https://csharp.keicode.com/basic/events.php
* https://qiita.com/laughter/items/e9cf666e0430acc39e95
