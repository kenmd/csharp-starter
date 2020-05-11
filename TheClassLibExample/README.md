# Class Library Example

Based on MS official doc and some blogs etc.


## Setup ClassLib and Test

* https://docs.microsoft.com/ja-jp/dotnet/core/testing/unit-testing-with-dotnet-test
* https://qiita.com/jnuank/items/e9aeb2d8c99d1e6f1081

```bash
# NOTE: Avoid using same name of existing package on nuget.org
# for example MyClassLibrary etc. will cause odd error
# https://bignited.net/2019/05/23/how-to-add-local-nuget-packages-to-your-net-core-project/
# When your local package ID matches a package ID
# on the online repository of nuget.org then
# the online repository takes precedence over your local package.
dotnet new sln -o TheClassLibExample
cd TheClassLibExample

# default is netstandard2.0 so use netcoreapp3.1
dotnet new classlib -f netcoreapp3.1 -o MyService
mv MyService/Class1.cs MyService/SampleCalcUtil.cs
code .
# add some code to the library

dotnet sln add ./MyService/MyService.csproj
dotnet new xunit -o MyService.Tests
dotnet sln add ./MyService.Tests/MyService.Tests.csproj
dotnet add ./MyService.Tests/MyService.Tests.csproj reference ./MyService/MyService.csproj
mv MyService.Tests/UnitTest1.cs MyService.Tests/SampleCalcUtilTest.cs
# update test and some code
```


## Unit Test

```bash
dotnet test
```


## Local NuGet Setup

* https://www.devtrends.co.uk/blog/creating-your-first-shared-library-in-.net-core
* https://medium.com/@churi.vibhav/creating-and-using-a-local-nuget-package-repository-9f19475d6af8

```bash
# check and update MyService/MyService.csproj
dotnet clean    # this may need when you retry
dotnet build
find . -name \*.nupkg
# will create MyService/bin/Debug/TheClassLibExample.MyService.0.1.5.nupkg

# from MS official doc
dotnet pack ./MyService/MyService.csproj -p:TargetFrameworks=netcoreapp3.1 --output ../LocalNuGetRepo

# Just for reference, maybe the above is same thing as below
# nuget add MyService/bin/Debug/TheClassLibExample.MyService.0.1.5.nupkg -source ../LocalNuGetRepo
```


## Install Local NuGet package

* https://stackoverflow.com/questions/43400069
* https://softchris.github.io/pages/dotnet-nuget.html#point-out-the-local-source

```bash
# run this in some other project
cd AWSLambda/MyFunction/src/MyFunction/

# add <RestoreSources> inside <PropertyGroup> in MyFunction.csproj
# <RestoreSources>$(RestoreSources);...(see the sample)</RestoreSources>

dotnet add package TheClassLibExample.MyService
# NOTE: you don't need --source option or --framework netcoreapp3.1

# or same thing
cd AWSLambda/MyFunction
dotnet add src/MyFunction package TheClassLibExample.MyService
```


## Uninstall NuGet package

```bash
dotnet remove src/MyFunction package TheClassLibExample.MyService
```


## Trouble Shooting

* error when run `dotnet add package TheClassLibExample.MyService`

```
Package 'TheClassLibExample' is incompatible with 'all' frameworks in the project
```

* make sure `MyService.csproj` has `<TargetFramework>` `netcoreapp3.1`
  - same as the project you use, in this example `MyFunction.csproj`
* `nuget update -self` and `brew install nuget` for latest version
* clear nuget cache

```
dotnet nuget locals all --clear
dotnet restore MyService
dotnet restore MyService.Tests
```

* you don't need `NuGet.config` as far as I tried
* but you need `<RestoreSources>` in `MyFunction.csproj`
* check other useful docs and blogs
  - https://softchris.github.io/pages/dotnet-nuget.html#add-the-package
  - https://bignited.net/2019/05/23/how-to-add-local-nuget-packages-to-your-net-core-project/


## Reference

* C# - Writing own class libraries
  - https://www.youtube.com/watch?v=ukBwNiim7i4
