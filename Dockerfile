FROM node:18-alpine as front-build
WORKDIR /app

COPY ./WEB/. ./

RUN yarn
RUN yarn build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore any dependencies (via nuget)
COPY . ./

RUN dotnet restore
# Copy the project files and build the release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out .
COPY --from=front-build /app/build ./wwwroot

# Set the environment variable to listen on port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "WebApi.dll"]