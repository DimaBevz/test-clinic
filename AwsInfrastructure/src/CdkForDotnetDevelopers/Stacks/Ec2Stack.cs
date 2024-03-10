using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Amazon.CDK.AWS.IAM;
using Constructs;

namespace CdkForDotnetDevelopers.Stacks;

public class Ec2Stack: Stack
{
    public Ec2Stack(Construct scope, string id, Ec2StackProps props = null) : base(scope, id, props)
    {
        var vpc = new Vpc(this, "ClinifyVpc", new VpcProps
        {
            NatGateways = 0,
            MaxAzs = 2,
        });

        var cluster = new Cluster(this, "ClinifyCluster", new ClusterProps
        {
            Vpc = vpc
        });

        var ecr = new Repository(this, "clinifybackendrepository", new RepositoryProps
        {
            RepositoryName = "clinifybackendrepositorydev"
        });
        
        var role = new Role(this, "ExecutionRole", new RoleProps()
        {
            AssumedBy = new ServicePrincipal("ecs-tasks.amazonaws.com")
        });
        
        role.AddToPolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Actions = new[] { "s3:*", "cognito:*", "cognito-identity:*","cognito-idp:*","cognito-sync:*","ecr:GetAuthorizationToken"  },
            Resources = new[] { "*" }
        }));
        
        cluster.AddCapacity("ASG", new AddCapacityOptions
        {
            AssociatePublicIpAddress = true,
            VpcSubnets = new SubnetSelection
            {
                Subnets = vpc.PublicSubnets,
            },
            InstanceType = new InstanceType("t2.micro"),
            MaxCapacity = 1, 
            MachineImage = EcsOptimizedImage.AmazonLinux2(AmiHardwareType.STANDARD)
        });
        
        var propss = new ApplicationLoadBalancedEc2ServiceProps
        {
            Cluster = cluster,
            DesiredCount = 1,
            OpenListener = true,
            TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
            {
                ExecutionRole = role,
                TaskRole = role,
                ContainerName = "backend",
                Environment = new Dictionary<string, string>
                {
                    {"ASPNETCORE_HTTP_PORTS", "80"},
                    {"ASPNETCORE_ENVIRONMENT", "Production"},
                    {"AWS__UserPoolId", props?.UserPool.UserPoolId},
                    {"AWS__Region", Region},
                    {"AWS__UserPoolClientId", props?.UserPoolClient.UserPoolClientId},
                    { "AWS_ACCESS_KEY_ID", "AKIAYS2NT6NZACKE75F4"},
                    { "AWS_SECRET_ACCESS_KEY", "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"},
                    { "AWS_REGION", Region}
                },
                ContainerPort = 80,
                Image = ContainerImage.FromRegistry("public.ecr.aws/ecs-sample-image/amazon-ecs-sample:latest")
            },
            MaxHealthyPercent = 200,
            MinHealthyPercent = 0,
            ListenerPort = 80,
            MemoryReservationMiB = 250,
            PublicLoadBalancer = true,
        };
        
        var developmetServiceProps = new ApplicationLoadBalancedEc2ServiceProps
        {
            Cluster = cluster,
            DesiredCount = 1,
            OpenListener = true,
            TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
            {
                ExecutionRole = role,
                TaskRole = role,
                ContainerName = "backend_dev",
                Environment = new Dictionary<string, string>
                {
                    {"ASPNETCORE_HTTP_PORTS", "80"},
                    {"ASPNETCORE_ENVIRONMENT", "Development"},
                    {"AWS__UserPoolId", props?.UserPoolDev.UserPoolId},
                    {"AWS__Region", Region},
                    {"AWS__UserPoolClientId", props?.UserPoolClientDev.UserPoolClientId},
                    { "AWS_ACCESS_KEY_ID", "AKIAYS2NT6NZACKE75F4"},
                    { "AWS_SECRET_ACCESS_KEY", "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"},
                    { "AWS_REGION", Region}
                },
                ContainerPort = 80,
                Image = ContainerImage.FromRegistry("public.ecr.aws/ecs-sample-image/amazon-ecs-sample:latest")
            },
            MaxHealthyPercent = 200,
            MinHealthyPercent = 0,
            ListenerPort = 80,
            MemoryReservationMiB = 250,
            PublicLoadBalancer = true,
        };
        
        var service = new ApplicationLoadBalancedEc2Service(this, "ClinifyBackEndService", propss);
        var developmentService = new ApplicationLoadBalancedEc2Service(this, "DevelopmentService", developmetServiceProps);
        
        _ = new CfnOutput(this, "clinifybackendrepositoryUri", new CfnOutputProps
        {
            Value = ecr.RepositoryUri
        });
        
        _ = new CfnOutput(this, "EcrRepositoryName", new CfnOutputProps
        {
            Value = ecr.RepositoryName
        });
        
        _ = new CfnOutput(this, "ClinifyBackEndServiceUri", new CfnOutputProps
        {
            Value = service.LoadBalancer.LoadBalancerDnsName
        });
        
        _ = new CfnOutput(this, "ClinifyBackEndServiceDevUri", new CfnOutputProps
        {
            Value = developmentService.LoadBalancer.LoadBalancerDnsName
        });
        
        _ = new CfnOutput(this, "ClinifyBackEndServiceName", new CfnOutputProps
        {
            Value = service.Service.ServiceName
        });
        
        _ = new CfnOutput(this, "ClinifyBackEndServiceDevName", new CfnOutputProps
        {
            Value = developmentService.Service.ServiceName
        });
        
        _ = new CfnOutput(this, "ClinifyBackEndClusterName", new CfnOutputProps
        {
            Value = service.Cluster.ClusterName
        });
        
        _ = new CfnOutput(this, "ClinifyBackTaskDefinitionArn", new CfnOutputProps
        {
            Value = service.TaskDefinition.TaskDefinitionArn
        });
        
        _ = new CfnOutput(this, "ClinifyBackTaskDefinitionDevArn", new CfnOutputProps
        {
            Value = developmentService.TaskDefinition.TaskDefinitionArn
        });
    } 
}
