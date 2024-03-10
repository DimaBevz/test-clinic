using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CdkForDotnetDevelopers
{
    public class MainResources : Stack
    {
        public readonly UserPool UserPool;
        public readonly UserPool UserPoolDev;
        public readonly UserPoolClient UserPoolClient;
        public readonly UserPoolClient UserPoolClientDev;
        
        internal MainResources(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            UserPool = new UserPool(this, "MyUserPool", new UserPoolProps
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
                },
                StandardAttributes = new StandardAttributes
                {
                    Email = new StandardAttribute(),
                    PhoneNumber = new StandardAttribute(),
                },
                CustomAttributes = new Dictionary<string, ICustomAttribute>
                {
                    {"role", new StringAttribute()}
                }
            });
            
            UserPoolDev = new UserPool(this, "UserPoolDevelopment", new UserPoolProps
            {
                UserPoolName = "DevelopmentUserPool",
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
                },
                StandardAttributes = new StandardAttributes
                {
                    Email = new StandardAttribute(),
                    PhoneNumber = new StandardAttribute(),
                },
                CustomAttributes = new Dictionary<string, ICustomAttribute>
                {
                    {"role", new StringAttribute()}
                }
            });
            
            _ = new CfnUserPoolGroup(this, "Admin", new CfnUserPoolGroupProps()
            {
                GroupName = "Admin",
                UserPoolId = UserPool.UserPoolId
            });
            
            _ = new CfnUserPoolGroup(this, "Provider", new CfnUserPoolGroupProps()
            {
                GroupName = "Provider",
                UserPoolId = UserPool.UserPoolId
            });
            
            _ = new CfnUserPoolGroup(this, "Patient", new CfnUserPoolGroupProps()
            {
                GroupName = "Patient",
                UserPoolId = UserPool.UserPoolId
            });
            
            _ = new CfnUserPoolGroup(this, "AdminDev", new CfnUserPoolGroupProps()
            {
                GroupName = "Admin",
                UserPoolId = UserPoolDev.UserPoolId
            });
            
            _ = new CfnUserPoolGroup(this, "ProviderDev", new CfnUserPoolGroupProps()
            {
                GroupName = "Provider",
                UserPoolId = UserPoolDev.UserPoolId
            });
            
            _ = new CfnUserPoolGroup(this, "PatientDev", new CfnUserPoolGroupProps()
            {
                GroupName = "Patient",
                UserPoolId = UserPoolDev.UserPoolId
            });
            
            UserPoolClient = new UserPoolClient(this, "MyUserPoolClient", new UserPoolClientProps
            {
                UserPool = UserPool
            });
            
            UserPoolClientDev = new UserPoolClient(this, "UserPoolClientDevelopment", new UserPoolClientProps
            {
                UserPool = UserPoolDev
            });

            _ = new Bucket(this, "clinifybuckett", new BucketProps
            {
                BucketName = "clinifybuckettdev",
                Versioned = false,
                AutoDeleteObjects = false,
                RemovalPolicy = RemovalPolicy.DESTROY
            });
        }
    }
}
