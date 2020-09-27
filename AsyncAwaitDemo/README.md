# Async Await Demo in C#

* Simple Task Async Await demo


## Key points

* `await Task` returns `Task` (note method signature is `async Task`)
* `await Task` is non blocking
* `Task.Wait()` and `Task.Result` is blocking (should be avoided)

## Async Programming Comparison

* APM (Asynchronous Programming Model): using `IAsyncResult`
* EAP (Event-based Asynchronous Programming): using `EventHandler`
* TAP (Task-based Asynchronous Pattern): using `Task`

## Setup

```bash
cd AsyncAwaitDemo
dotnet new sln -n AsyncAwaitDemo
dotnet new console -n AsyncDemoApp
dotnet new console -n CustomAsyncApp
dotnet sln AsyncAwaitDemo.sln add **/*.csproj

cd AsyncDemoApp
dotnet add package Microsoft.CodeAnalysis.FxCopAnalyzers
```

## Run

```bash
cd AsyncDemoApp   # or CustomAsyncApp
dotnet restore
dotnet build
COMPlus_DebugWriteToStdErr=1 dotnet run
```

__CustomAsyncApp Output__

```
Await Start
Sleep Start in Foo
Sleep End in Foo
Await End
Await Start 2
Sleep Start in Bar
Await Start 2
Sleep Start in Bar
Sleep End in Bar
Sleep End in Bar
Await End 2: 123
Await End 2: 123
```

## Reference

* https://csharp.keicode.com/basic/async.php
* await WebClient.DownloadStringTaskAsync waits forever
  - https://stackoverflow.com/questions/34430868
* ConfigureAwait(false)
  - https://qiita.com/mxProject/items/e2b2271fd26cfc8b059c
* C# Taskの待ちかた集
  - https://qiita.com/takutoy/items/d45aa736ced25a8158b3
* Async/Await best practice
  - https://docs.microsoft.com/ja-jp/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
