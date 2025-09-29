# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore AvroMcpConvCommit.sln

# Build and publish
RUN dotnet publish src/AvroMcpConvCommit.Presentation/AvroMcpConvCommit.Presentation.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "AvroMcpConvCommit.dll"]