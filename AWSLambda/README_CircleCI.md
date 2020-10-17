# CircleCI

## Setup

```bash
# initial cli setup
brew install circleci
circleci setup

# check yml
circleci config validate -c .circleci/config.yml

# run locally
# c.f. https://circleci.com/docs/2.0/local-cli/#running-a-job
# circleci config process .circleci/config.yml > /tmp/process.yml
circleci local execute -c .circleci/config.yml --job build
```

## Sample Config

* deploy to `test` immediately, to `prod` after approval by Slack message
* need setup of `Context` in CircleCI
  - `deployer-xx`: `AWS_ACCESS_KEY_ID` `AWS_SECRET_ACCESS_KEY` `AWS_DEFAULT_REGION`
  - `slack-webhook`: webhook URL
* also need `aws-lambda-tools-test.json` `aws-lambda-tools-prod.json`

```yaml
version: 2.1

orbs:
  slack: circleci/slack@3.4.2

executors:
  dotnetcore31:
    docker:
      - image: mcr.microsoft.com/dotnet/core/sdk:3.1
    environment:
      # speed up dotnet core builds
      # https://blog.shibayan.jp/entry/20161210/1481337173
      DOTNET_SKIP_FIRST_TIME_EXPERIENCE: "true"
      NUGET_XMLDOC_MODE: skip

workflows:
  build_and_deploy:
    jobs:
      - build
      - deploy: # immediately
          requires: [build]
          name: deploy_test
          env: test
          context: deployer-test
      - slack/approval-notification:
          message: Pending approval
          mentions: here
          color: "#F5E911"
          requires: [build]
          context: slack-webhook
          filters:
            branches: { only: [master] }
      - hold_prod:
          type: approval
          requires: [build]
          filters:
            branches: { only: [master] }
      - deploy: # after approval
          requires: [hold_prod]
          name: deploy_prod
          env: prod
          context: deployer-prod
          filters:
            branches: { only: [master] }

jobs:
  build:
    executor: dotnetcore31
    steps:
      - checkout
      - run: dotnet restore MyFunction/src/MyFunction
      - run: dotnet test MyFunction
  deploy:
    executor: dotnetcore31
    parameters:
      env:
        type: enum
        enum: ["test", "prod"]
    steps:
      - checkout
      - run: apt-get update && apt-get install -y zip
      - run: dotnet tool install --global Amazon.Lambda.Tools --version 4.0.0
      # https://circleci.com/docs/2.0/env-vars/#using-parameters-and-bash-environment
      - run: echo "export PATH=$HOME/.dotnet/tools:$PATH" >> $BASH_ENV
      - run:
          name: deploy-MyFunction
          working_directory: "MyFunction/src/MyFunction"
          command: dotnet lambda deploy-function --config-file aws-lambda-tools-<< parameters.env >>.json
```
