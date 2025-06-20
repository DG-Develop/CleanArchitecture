# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Core/Domain/ECommerce.Domain.csproj src/Core/Domain/
COPY src/Core/Application/ECommerce.Application.csproj src/Core/Application/
COPY src/Infrastructure/Persistence/ECommerce.Persistence.csproj src/Infrastructure/Persistence/
COPY src/Infrastructure/Shared/ECommerce.Shared.csproj src/Infrastructure/Shared/
COPY src/Presentation/WebAPI/ECommerce.API.csproj src/Presentation/WebAPI/

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src
RUN dotnet publish src/Presentation/WebAPI/ECommerce.API.csproj -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]
