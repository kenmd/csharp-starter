using System.Collections.Generic;

using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.Events.Targets;


namespace CdkWorkshop
{
    public class CdkWorkshopStack : Stack
    {
        internal CdkWorkshopStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            Role role = createLambdaRole();

            new CfnOutput(this, "LambdaRoleName", new CfnOutputProps()
            {
                Value = role.RoleName
            });

            var function = findFunctionByName("MyFunction");

            new CfnOutput(this, "MyFunction.RoleArn", new CfnOutputProps()
            {
                Value = function.FunctionName
            });

            var Rule = createRule(function);

            new CfnOutput(this, "RuleName", new CfnOutputProps()
            {
                Value = Rule.RuleName
            });
        }

        private Role createLambdaRole()
        {
            Role role = new Role(this, "LambdaRole", new RoleProps
            {
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });

            role.AddManagedPolicy(
                ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaBasicExecutionRole")
            );

            role.AddManagedPolicy(
                ManagedPolicy.FromAwsManagedPolicyName("service-role/AWSLambdaVPCAccessExecutionRole")
            );

            return role;
        }

        internal IFunction findFunctionByName(string name)
        {
            var region = Stack.Of(this).Region;
            var account = Stack.Of(this).Account;
            var arn = $"arn:aws:lambda:{region}:{account}:function:{name}";

            return Function.FromFunctionArn(this, name, arn);
        }

        private Rule createRule(IFunction function)
        {
            RuleProps ruleProps = new RuleProps
            {
                Description = "10:00 JST MON-FRI",
                Schedule = Schedule.Cron(new CronOptions
                {
                    Hour = "01",
                    Minute = "00",
                    WeekDay = "MON-FRI"
                }),
            };
            Rule rule = new Rule(this, "Rule", ruleProps);

            LambdaFunctionProps props = new LambdaFunctionProps();
            props.Event = RuleTargetInput.FromText("Hello");

            rule.AddTarget(new LambdaFunction(function, props));

            return rule;
        }
    }
}
