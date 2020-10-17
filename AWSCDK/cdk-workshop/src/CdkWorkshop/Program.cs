using Amazon.CDK;
using SysEnv = System.Environment;

namespace CdkWorkshop
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var props = new StackProps { Env = makeEnv() };

            new CdkWorkshopStack(app, "CdkWorkshopStack", props);

            app.Synth();
        }

        // https://qiita.com/tmisuoka0423/items/325dfe91e7073b358435
        // https://docs.aws.amazon.com/cdk/latest/guide/environments.html
        private static Environment makeEnv(string account = null, string region = null)
        {
            return new Environment
            {
                Account = account ?? SysEnv.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                Region = region ?? SysEnv.GetEnvironmentVariable("CDK_DEFAULT_REGION")
            };
        }
    }
}
