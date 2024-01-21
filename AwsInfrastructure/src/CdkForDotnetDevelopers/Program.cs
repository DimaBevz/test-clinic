using Amazon.CDK;
using CdkForDotnetDevelopers.Stacks;

namespace CdkForDotnetDevelopers
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CdkForDotnetDevelopersStack(app, "CdkForDotnetDevelopersStack", new StackProps{});
            new FargateStack(app, "ECSCluster", new StackProps{});
            app.Synth();
        }
    }
}
