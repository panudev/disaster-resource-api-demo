# 1. Base image for runtime (use ASP.NET Core 9.0)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Build image use SDK 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 3. Copy sln + csproj to restore
COPY *.sln .
COPY *.csproj ./
RUN dotnet restore

# 4. Copy source All and publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# 5. Final image run to api
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DisasterResourceAllocationAPI.dll"]
