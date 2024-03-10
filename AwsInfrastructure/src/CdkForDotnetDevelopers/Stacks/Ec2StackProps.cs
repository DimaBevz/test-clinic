using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;

namespace CdkForDotnetDevelopers.Stacks;

public class Ec2StackProps: StackProps
{
    public UserPool UserPool;
    public UserPool UserPoolDev;
    public UserPoolClient UserPoolClient;
    public UserPoolClient UserPoolClientDev;
}
