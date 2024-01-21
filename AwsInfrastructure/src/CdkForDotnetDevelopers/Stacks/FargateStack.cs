using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Amazon.CDK.AWS.IAM;
using Constructs;

namespace CdkForDotnetDevelopers.Stacks;

public class FargateStack: Stack
{
    public FargateStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
        var vpc = new Vpc(this, "ClinifyVpc", new VpcProps
        {
            MaxAzs = 3
        });

        var cluster = new Cluster(this, "ClinifyCluster", new ClusterProps
        {
            Vpc = vpc
        });

        var ecr = new Repository(this, "clinifybackendrepository", new RepositoryProps
        {
            RepositoryName = "clinifybackendrepository"
        });
        
        new CfnOutput(this, "clinifybackendrepositoryUri", new CfnOutputProps
        {
            Value = ecr.RepositoryUri
        });
        
        new CfnOutput(this, "EcrRepositoryName", new CfnOutputProps
        {
            Value = ecr.RepositoryName
        });
        var propss = new ApplicationLoadBalancedFargateServiceProps
        {
            Cluster = cluster,
            DesiredCount = 1,
            OpenListener = true,
            TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
            {
                ContainerName = "backend",
                Environment = new Dictionary<string, string>
                {
                    {"ASPNETCORE_HTTP_PORTS", "80"},
                    {"ASPNETCORE_ENVIRONMENT", "Development"}
                },
                ContainerPort = 80,
                Image = ContainerImage.FromRegistry("public.ecr.aws/ecs-sample-image/amazon-ecs-sample:latest")
            },
            ListenerPort = 80,
            MemoryLimitMiB = 1024,
            PublicLoadBalancer = true
        };

        var service = new ApplicationLoadBalancedFargateService(this, "ClinifyBackEndService", propss);
        
        new CfnOutput(this, "ClinifyBackEndServiceUri", new CfnOutputProps
        {
            Value = service.LoadBalancer.LoadBalancerDnsName
        });
        
        new CfnOutput(this, "ClinifyBackEndServiceName", new CfnOutputProps
        {
            Value = service.Service.ServiceName
        });
        
        new CfnOutput(this, "ClinifyBackEndClusterName", new CfnOutputProps
        {
            Value = service.Cluster.ClusterName
        });
        
        new CfnOutput(this, "ClinifyBackTaskDefinitionArn", new CfnOutputProps
        {
            Value = service.TaskDefinition.TaskDefinitionArn
        });
    } 
}
