# C# Starter using VSCode

* following this video "Intro to VSCode for C# Developers"
  - https://www.youtube.com/watch?v=r5dtl9Uq9V0


## Setup

* install .NET Core 3.1 SDK (v3.1.201) for Mac
  - https://dotnet.microsoft.com/download
  - check `dotnet --version` to see `3.1.201`
* install VSCode extension
  - `ms-dotnettools.csharp`
  - `ms-mssql.mssql`
  - `jmrog.vscode-nuget-package-manager`
  - `eamodio.gitlens` (optional)
  - `vscode-icons-team.vscode-icons` (optional)


## Create App

* create solution and projects from the video

```bash
cd VSCodeIntro/
# create solution
dotnet new sln -n "VSCodeIntroSln"
# create project
dotnet new console -n "IntroUI"
# create library
dotnet new classlib -n "IntroLibrary"
# add project and library to solution
dotnet sln VSCodeIntroSln.sln add **/*.csproj
# create reference to library
dotnet add IntroUI/IntroUI.csproj reference IntroLibrary/IntroLibrary.csproj
# open each directories and answer Yes when asked required assets missing
code IntroUI/
code .
# check launch.json have program IntroUI.dll
```

__NuGet__

```bash
# NuGet from CLI
cd IntroLibrary/
dotnet add package Dapper
# add Dapper to IntroLibrary.csproj copy <ItemGroup>...</ItemGroup>
# paste to IntroUI.csproj and click Restore as suggested

# NuGet from View > Command Palette
# NuGet Package Manager: Add Package > SeriLog
# select the latest version then click Restore
```

* In case VSCode didn't suggest to restore, run manually

```bash
cd VSCodeIntro/
dotnet restore
```

__Editing Shortcuts__

* `cw`: `System.Console.WriteLine();`
* `prop`: class property


## Database Connection

* install from NuGet Package Manager
  - `Dapper`
  - `Microsoft.Data.SqlClient`
  - `System.Configuration.ConfigurationManager`
* follow the Getting Started at https://dapper-tutorial.net/


## Run

* VSCode menu Run > Run Without Debugging
* You should see the query result


## Reference

* Official Setup Guide and Quick Tutorials
  - Using .NET Core in Visual Studio Code
    - https://code.visualstudio.com/docs/languages/dotnet
  - Working with C#
    - https://code.visualstudio.com/docs/languages/csharp
  - Hello World in 10 minutes
    - https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro
* JAVAを数年触ったエンジニアがC#を学んで感じたこと
  - https://qiita.com/yShig/items/068090e8c55b1f5278b9
