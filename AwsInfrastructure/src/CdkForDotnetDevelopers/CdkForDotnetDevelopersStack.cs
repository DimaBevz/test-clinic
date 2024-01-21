using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CdkForDotnetDevelopers
{
    public class CdkForDotnetDevelopersStack : Stack
    {
        internal CdkForDotnetDevelopersStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var userPool = new UserPool(this, "MyUserPool", new UserPoolProps
            {
                UserPoolName = "MyUserPool",
                SelfSignUpEnabled = true, 
                SignInAliases = new SignInAliases { Email = true }, 
                AutoVerify = new AutoVerifiedAttrs { Email = true },
                PasswordPolicy = new PasswordPolicy
                {
                    MinLength = 8,
                    RequireLowercase = true,
                    RequireDigits = true,
                    RequireSymbols = true,
                    RequireUppercase = true
                }
            });
            
            var userPoolDomain = new UserPoolDomain(this, "MyUserPoolDomain", new UserPoolDomainProps
            {
                UserPool = userPool,
                // Укажите желаемое доменное имя
                CognitoDomain = new CognitoDomainOptions { DomainPrefix = "clinifytestdomain" }
            });
            
            var adminGroup = new CfnUserPoolGroup(this, "Admin", new CfnUserPoolGroupProps()
            {
                GroupName = "Admin",
                UserPoolId = userPool.UserPoolId
            });
            
            var providerGroup = new CfnUserPoolGroup(this, "Provider", new CfnUserPoolGroupProps()
            {
                GroupName = "Provider",
                UserPoolId = userPool.UserPoolId
            });
            
            var patientGroup = new CfnUserPoolGroup(this, "Patient", new CfnUserPoolGroupProps()
            {
                GroupName = "Patient",
                UserPoolId = userPool.UserPoolId
            });
            
            // Display the Cognito Group ARN in CloudFormation outputs
            new CfnOutput(this, "AdminGroupArn", new CfnOutputProps { Value = adminGroup.GroupName });
            new CfnOutput(this, "ProviderGroupArn", new CfnOutputProps { Value = providerGroup.GroupName });
            new CfnOutput(this, "PatientGroupArn", new CfnOutputProps { Value = patientGroup.GroupName });
            
            // Вывод атрибутов пула пользователей
            new CfnOutput(this, "UserPoolId", new CfnOutputProps { Value = userPool.UserPoolId });
            new CfnOutput(this, "UserPoolArn", new CfnOutputProps { Value = userPool.UserPoolArn });

            // Вывод атрибутов домена Hosted UI
            new CfnOutput(this, "UserPoolDomain", new CfnOutputProps { Value = userPoolDomain.DomainName });
            new CfnOutput(this, "UserPoolDomainUrl", new CfnOutputProps { Value = $"https://{userPoolDomain.DomainName}.auth.{this.Region}.amazoncognito.com" });
           
            var userPoolClient = new UserPoolClient(this, "MyUserPoolClient", new UserPoolClientProps
            {
                UserPool = userPool
            });

            new CfnOutput(this, "UserPoolClientId", new CfnOutputProps { Value = userPoolClient.UserPoolClientId });
            
            var s3Bucket = new Bucket(this, "demobucket", new BucketProps
            {
                Versioned = true,
                AutoDeleteObjects = true,
                RemovalPolicy = RemovalPolicy.DESTROY
            });
            new CfnOutput(this, "BucketArn", new CfnOutputProps { Value = s3Bucket.BucketArn });
        }
    }
}
