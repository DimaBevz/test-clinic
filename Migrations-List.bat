@echo off

set "AWS_ACCESS_KEY_ID=a" & set "AWS_SECRET_ACCESS_KEY=a" & set "AWS_REGION=eu-east-1" & dotnet-ef migrations list -p Infrastructure\Infrastructure.Persistence -s WebApi
PAUSE