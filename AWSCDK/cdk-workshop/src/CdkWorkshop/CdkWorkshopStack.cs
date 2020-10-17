using System.Collections.Generic;

using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.EC2;

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

            var vpcId = System.Environment.GetEnvironmentVariable("VPC_ID");
            var theVpc = findVpcByVpcId(vpcId);
            var sg = createLambdaSecurityGroup(theVpc);

            new CfnOutput(this, "LambdaSecurityGroupName", new CfnOutputProps()
            {
                Value = sg.SecurityGroupName
            });

            var function = findFunctionByName("MyFunction");

            new CfnOutput(this, "FunctionName", new CfnOutputProps()
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

        private SecurityGroup createLambdaSecurityGroup(IVpc theVpc)
        {
            var sg = new SecurityGroup(this, "LambdaSG", new SecurityGroupProps()
            {
                Vpc = theVpc,
                SecurityGroupName = "LambdaSG",
            });

            return sg;
        }

        private IVpc findVpcByVpcId(string vpcId)
        {
            // How to import existing VPC
            // https://qiita.com/kai_kou/items/e35fd8c6af7dff9f2624
            // see difference of fromLookup and fromVpcAttributes
            // https://garbe.io/blog/2019/09/20/hey-cdk-how-to-use-existing-resources/
            var vpc = Vpc.FromLookup(this, "ExistingVPC", new VpcLookupOptions()
            {
                VpcId = vpcId
            });

            return vpc;
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

            // if the input is json
            // var jsonDict = new Dictionary<string, string>
            // {
            //     { "Key", "Value" },
            // };
            // props.Event = RuleTargetInput.FromObject(jsonDict);

            props.Event = RuleTargetInput.FromText("Hello");

            rule.AddTarget(new LambdaFunction(function, props));

            return rule;
        }
    }
}
