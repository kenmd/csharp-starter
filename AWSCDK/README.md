# AWS CDK

Quick start CDK sample code to add Event to run Lambda

* add CloudWatch Event to trigger the Lambda by `cdk deploy`
* refer existing Lambda deployed by `dotnet lambda deploy-function`
* using Official CDK Workshop: https://cdkworkshop.com/40-dotnet.html


## Setup CDK

```bash
# setup node by nodenv https://github.com/nodenv/nodenv
nodenv --version
> nodenv 1.3.2

nodenv install 12.16.2
nodenv global 12.16.2

nodenv versions
> * 12.16.2
node --version
> v12.16.2

# Installing the AWS CDK
npm install -g aws-cdk

# or upgrade
npm outdated -g
npm update -g

cdk --version
> 1.36.0 (build 47c9919)
# in case command not found error, try "nodenv rehash"
```


## Create initial app

```bash
cdk init sample-app --language csharp
# check generated README to find useful commands

dotnet restore src/CdkWorkshop
dotnet build src/CdkWorkshop

export AWS_PROFILE=your-profile
cdk synth

# (optional?) the first time you deploy
# create S3 bucket cdktoolkit-... and Cfn Stack CDKToolkit
# cdk bootstrap

# create CloudFormation Stack
cdk deploy
```


## Develop

```bash
cd src/CdkWorkshop/
# NOTE: cdk init app create simple app without SNS SQS
dotnet remove package Amazon.CDK.AWS.SNS.Subscriptions
dotnet remove package Amazon.CDK.AWS.SNS
dotnet remove package Amazon.CDK.AWS.SQS
dotnet add package Amazon.CDK.AWS.Lambda
dotnet add package Amazon.CDK.AWS.Events
dotnet add package Amazon.CDK.AWS.Events.Targets

# check and update project package
dotnet list src/CdkWorkshop package --outdated
> Amazon.CDK                         1.39.0   1.39.0   1.68.0
> Amazon.CDK.AWS.Events              1.39.0   1.39.0   1.68.0
> Amazon.CDK.AWS.Events.Targets      1.39.0   1.39.0   1.68.0
> Amazon.CDK.AWS.IAM                 1.39.0   1.39.0   1.68.0
> Amazon.CDK.AWS.Lambda              1.39.0   1.39.0   1.68.0
# same add command to update
dotnet add src/CdkWorkshop package Amazon.CDK
dotnet add src/CdkWorkshop package Amazon.CDK.AWS.Events
dotnet add src/CdkWorkshop package Amazon.CDK.AWS.Events.Targets
dotnet add src/CdkWorkshop package Amazon.CDK.AWS.IAM
dotnet add src/CdkWorkshop package Amazon.CDK.AWS.Lambda

# update the code in CdkWorkshopStack.cs

cdk diff
cdk deploy
```

* it seems there is chicken and egg problem around lambda permission
* first you create lambda role from cdk, next dotnet deploy lambda
* and create rule from cdk, then manually add trigger from console


## Clean up

```bash
cdk destroy
```
