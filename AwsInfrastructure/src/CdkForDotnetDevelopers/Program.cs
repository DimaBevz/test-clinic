using System.Collections.Generic;
using Amazon.CDK;
using CdkForDotnetDevelopers.Stacks;

namespace CdkForDotnetDevelopers
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var resources = new MainResources(app, "CdkForDotnetDevelopersStack", new StackProps()
            {
                StackName = "CdkForDotnetDevelopersStack",
            });
            _ = new Ec2Stack(app, "ECSCluster", new Ec2StackProps
            {
                UserPoolClient = resources.UserPoolClient,
                UserPoolClientDev = resources.UserPoolClientDev,
                UserPool = resources.UserPool,
                UserPoolDev = resources.UserPoolDev,
            });
            
            app.Synth();
        }
    }
}
