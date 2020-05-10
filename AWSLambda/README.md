# AWS Lambda C#

* try sample code in Lambda C# with official docs
  - https://aws.amazon.com/premiumsupport/knowledge-center/build-lambda-deployment-dotnet/
  - https://docs.aws.amazon.com/lambda/latest/dg/csharp-package-cli.html


## Setup

```bash
# check installed templates
dotnet new -all

# install Lambda templates
dotnet new -i Amazon.Lambda.Templates

# create Lambda
dotnet new lambda.EmptyFunction --name MyFunction

cd MyFunction/src/MyFunction
dotnet restore
# or click restore as VSCode suggested

cd MyFunction/
code . 

# for logging test, try adding one line
# LambdaLogger.Log("Input: " + input);
# in Function.cs FunctionHandler(...)

# run Test
export $(cat env.json | jq -r '.MyFunction | to_entries[] | "\(.key)=\(.value)"')
dotnet test test/MyFunction.Tests
# or open FunctionTest.cs in VSCode and click Run Test
```


## Build and Deploy

```bash
# install dotnet-lambda
dotnet tool install -g Amazon.Lambda.Tools
# and update PATH as suggested ($HOME/.dotnet/tools)
# or update if already installed
# dotnet tool update -g Amazon.Lambda.Tools

export AWS_PROFILE=your_profile
cd MyFunction/src/MyFunction

# check available options for deploy
dotnet lambda help deploy-function
# you can add deploy options in aws-lambda-tools-defaults.json
# https://aws.amazon.com/blogs/developer/deploying-net-core-aws-lambda-functions-from-the-command-line/

# build and deploy
dotnet lambda deploy-function MyFunction # --function-role your_role
# select role or create new role with followings
# AWSLambdaBasicExecutionRole
# AWSLambdaVPCAccessExecutionRole

# verify the Lambda created in console

# test run in a production environment
dotnet lambda invoke-function MyFunction --payload "Just Checking If Everything is OK"
> "JUST CHECKING IF EVERYTHING IS OK"

# check CloudWatch log "Input: Just Checking If Everything is OK"
```


## Adding SQL Server connection

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
dotnet add package Dapper # for example
```


## Clean up

```bash
dotnet lambda delete-function MyFunction
```
