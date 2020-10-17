# AWS Lambda C# Quick Start

Quick start guide of Lambda .Net by copy & paste

## Create Empty Lambda

```bash
# verify dotnet command
dotnet --version
3.1.401

# list installed templates to check Lambda Templates
dotnet new --list

# if not exist, install Lambda templates
dotnet new --install Amazon.Lambda.Templates

# if exist, check updates
# dotnet new --update-check

# create empty Lambda from template
dotnet new lambda.EmptyFunction --name MyFunction
```

## Create Solution

add `.sln` file for convenience of build and test

```bash
cd MyFunction

dotnet new sln -n MyFunctionSln
dotnet sln MyFunctionSln.sln add src/MyFunction/MyFunction.csproj
dotnet sln MyFunctionSln.sln add test/MyFunction.Tests/MyFunction.Tests.csproj
dotnet add test/MyFunction.Tests/MyFunction.Tests.csproj \
    reference src/MyFunction/MyFunction.csproj
```

## Add Packages

```bash
cd MyFunction/src/MyFunction

# add packages for example like this
dotnet add package Microsoft.CodeAnalysis.FxCopAnalyzers
# etc...

# check outdated packages
dotnet list package --outdated
> Amazon.Lambda.Serialization.SystemTextJson      1.0.0   1.0.0   2.0.2

# upgrade package (execute same "dotnet add package")
dotnet add package Amazon.Lambda.Serialization.SystemTextJson
```

## Adjust FxCopAnalyzers Rule

```bash
# create GlobalSuppressions.cs
touch MyFunction/src/MyFunction/GlobalSuppressions.cs

dotnet restore MyFunction
dotnet build MyFunction
# check FxCopAnalyzers warnings and update GlobalSuppressions.cs
```

## Run Test

```bash
dotnet restore MyFunction
dotnet test MyFunction
```

## Run Coverage Report

```bash
cd MyFunction/test/MyFunction.Tests
dotnet add package coverlet.msbuild

# nuget package ReportGenerator did not work as far as I tried
# so instead of dotnet add package ReportGenerator
# use globaltool instead
dotnet tool install -g dotnet-reportgenerator-globaltool
```

```bash
cd MyFunction

# quick test to show coverage numbers by json
dotnet test test/MyFunction.Tests \
    /p:CollectCoverage=true \
    /p:CoverletOutput=../../coverage/

# generate detail report by xml
dotnet test test/MyFunction.Tests \
    /p:CollectCoverage=true \
    /p:CoverletOutput=../../coverage/ \
    /p:CoverletOutputFormat=opencover

# Tips: use Exclude option to exclude some files
#   /p:Exclude=\"[SomeLib*]*,[*]*SomeClass.*\"

reportgenerator \
    "-reports:coverage/*.xml" \
    "-reporttypes:HTMLInline" \
    "-targetdir:coverage/report"

open coverage/report/index.html
```

## Deploy

```bash
# check Lambda Tools
dotnet tool list

# if not exist, install Lambda Tools
dotnet tool install -g Amazon.Lambda.Tools
# and update PATH as suggested ($HOME/.dotnet/tools)

# if exist, upgrade
# dotnet tool update -g Amazon.Lambda.Tools
```

```bash
# check available options for deploy
dotnet lambda help deploy-function

# you can add deploy options in aws-lambda-tools-defaults.json
# for example you can add "function-name" instead of "--function-name" option
# https://aws.amazon.com/blogs/developer/deploying-net-core-aws-lambda-functions-from-the-command-line/

# before deploy, update values in aws-lambda-tools-defaults.json

export AWS_PROFILE=your-profile

cd MyFunction/src/MyFunction
dotnet lambda deploy-function --function-name MyFunction

# select existing role or
# 13) *** Create new IAM Role ***
# Enter name of the new IAM Role:
# LambdaRoleMyFunction
# Select IAM Policy to attach to the new role and grant permissions
# 8) AWSLambdaBasicExecutionRole

# verify the Lambda created in console
```

## Run

```bash
dotnet lambda invoke-function MyFunction --payload "Hello"
# this should return "HELLO"
```

## Adding SQLServer connection

* Command Palette > NuGet Package Manager > search and add
  - `Microsoft.Data.SqlClient`
  - `Dapper`
  - `Dapper.SimpleCRUD`
* update variables in `aws-lambda-tools-defaults.json`
  - `function-subnets`
  - `function-security-groups`
  - `environment-variables`
* You should encrypt DB password
  - see https://www.1strategy.com/blog/2019/02/06/connecting-to-an-rds-database-with-lambda/

```bash
# also possible to add from cli
cd src/MyFunction/
dotnet add package Microsoft.Data.SqlClient
dotnet add package Dapper
dotnet add package Dapper.SimpleCRUD

# start local SQL Server in local-database/local-mssql
# run migration in local-database/flyway-mssql

# run Test
export $(cat env.json | jq -r '.MyFunction | to_entries[] | "\(.key)=\(.value)"')
dotnet test test/MyFunction.Tests
# or open FunctionTest.cs in VSCode and click Run Test

# when deploy, you will need IAM Role with
# AWSLambdaBasicExecutionRole
# AWSLambdaVPCAccessExecutionRole
```

## Clean up

```bash
dotnet lambda delete-function MyFunction
```

## Reference

* Lambda .Net official docs
  - https://aws.amazon.com/premiumsupport/knowledge-center/build-lambda-deployment-dotnet/
  - https://docs.aws.amazon.com/lambda/latest/dg/csharp-package-cli.html
* About coverage report
  - https://github.com/coverlet-coverage/coverlet
  - https://github.com/danielpalme/ReportGenerator
  - https://mookiefumi.com/2019-10-20-unit-testing-tools-using-your-MacOS-terminal
