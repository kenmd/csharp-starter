# AWS Lambda Setup with .NET 5

* Download .NET 5.0
  - https://dotnet.microsoft.com/download

```bash
dotnet --version
5.0.101
sw_vers 
ProductName:	Mac OS X
ProductVersion:	10.15.7
```

## Create Empty Function and Test

```bash
# check lambda templates
dotnet new --list | grep EmptyFunction
> Lambda Empty Function                           lambda.EmptyFunction
> Lambda Empty Function (.NET 5 Container Image)  lambda.image.EmptyFunction
dotnet new --update-check
# install lambda templates
dotnet new -i Amazon.Lambda.Templates

# create hello world base code from template
dotnet new lambda.image.EmptyFunction --output DotNet5 --region ap-northeast-1

# add solution file (DotNet5Sln.sln)
cd DotNet5
dotnet new sln -n DotNet5Sln
dotnet sln DotNet5Sln.sln add src/DotNet5/DotNet5.csproj
dotnet sln DotNet5Sln.sln add test/DotNet5.Tests/DotNet5.Tests.csproj
# dotnet add test/DotNet5.Tests/DotNet5.Tests.csproj reference src/DotNet5/DotNet5.csproj

# run test
dotnet restore
dotnet build
dotnet test
```

## Upgrade Packages

```bash
# check
dotnet list package --outdated
# upgrade xunit for example
dotnet add test/DotNet5.Tests package xunit
```

## Deploy and Execute

```bash
# check amazon.lambda.tools
dotnet tool list -g
> amazon.lambda.tools   5.0.0  dotnet-lambda
# install deploy tools
dotnet tool install -g Amazon.Lambda.Tools
# or upgrade the tools
dotnet tool update -g Amazon.Lambda.Tools

# deploy
export AWS_PROFILE=some-profile
cd src/DotNet5
dotnet lambda deploy-function
# this will run these commands before push it to ECR
# dotnet publish -c Release -r linux-x64
# docker build -t dotnet5:latest .

> Enter Function Name:
DotNet5
> Select IAM Role that to provide AWS credentials to your code:
   15) *** Create new IAM Role ***
lambda-role-first-deploy-test
> Select IAM Policy to attach to the new role and grant permissions
    8) AWSLambdaBasicExecutionRole
> ...............  Done
> New Lambda function created

# Execute
dotnet lambda invoke-function DotNet5 --payload "Hello"

> Payload:
> {"Lower":"hello","Upper":"HELLO"}
```

## Run container Locally

```bash
# build docker image
# ("dotnet lambda deploy-function" will do this for you)
cd src/DotNet5
dotnet publish -c Release -r linux-x64
docker build -t dotnet5:latest .

# starting the Lambda function locally
docker run -p 9000:8080 dotnet5:latest "DotNet5::DotNet5.Function::FunctionHandler"

# send events into the local running Lambda function
curl -XPOST "http://localhost:9000/2015-03-31/functions/function/invocations" -d '"hello world"'
> {"Lower":"hello world","Upper":"HELLO WORLD"}
```

## Test using Amazon.Lambda.TestTool-5.0

```bash
dotnet tool install --global Amazon.Lambda.TestTool-5.0

cd src/DotNet5
# start with Web UI at http://localhost:5050/
dotnet lambda-test-tool-5.0
# type in Function Input: "Foo" and click Execute Function
# Response will be {"Lower":"foo","Upper":"FOO"}

# start without UI
dotnet lambda-test-tool-5.0 --no-ui
```

## Debug using VSCode

* set some debug points (in `FunctionHandler()` of `Function.cs`)
* `Run` > `Start Debugging`
* it will open web UI http://localhost:5050
* type in `Function Input`: `"Foo"`
* click `Execute Function`
* it will stop at the debug point
* see also `.vscode/launch.json`

## Additional Setup

### enable nullable

* add this in `<PropertyGroup>` of `.csproj`

```xml
    <!-- https://stackoverflow.com/questions/55492214 -->
    <Nullable>enable</Nullable>
    <WarningsAsErrors>CS8600;CS8602;CS8603</WarningsAsErrors>
```

### add code analysis

* `dotnet add src/DotNet5 package Microsoft.CodeAnalysis.NetAnalyzers`
* add this in `<PropertyGroup>` of `.csproj`

```xml
    <!-- https://docs.microsoft.com/en-us/visualstudio/code-quality/migrate-from-fxcop-analyzers-to-net-analyzers?view=vs-2019 -->
    <AnalysisMode>AllEnabledByDefault</AnalysisMode>
```

## Reference

* .NET 5 AWS Lambda Support with Container Images
  - Getting started from the .NET CLI
  - https://aws.amazon.com/blogs/developer/net-5-aws-lambda-support-with-container-images/
* auto generated project readme `src/DotNet5/Readme.md`
* Lambda Test Tool
  - https://github.com/aws/aws-lambda-dotnet/tree/master/Tools/LambdaTestTool
