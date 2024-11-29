# Stage 1: Build all dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["WebShop.Api/WebShop.Api.csproj", "WebShop.Api/"]
COPY ["Webshop.Application/WebShop.Application.csproj", "WebShop.Application/"]
COPY ["WebShop.Core/WebShop.Core.csproj", "WebShop.Core/"]
COPY ["WebShop.Instrastructure/WebShop.Instrastructure.csproj", "WebShop.Instrastructure/"]
COPY ["WebShopTests/WebShopTests.csproj", "WebShopTests/"]

RUN dotnet restore "WebShop.Api/WebShop.Api.csproj"

# Copy the full solution
COPY . .

# Build all projects
RUN dotnet build "WebShop.Api/WebShop.Api.csproj" -c Release

# Stage 2: Run tests
FROM build AS test
WORKDIR /src/WebShopTests

# Run tests and ensure they pass
RUN dotnet test "WebShopTests.csproj" --no-build --logger:trx --results-directory /src/TestResults

# Stage 3: Publish WebShop (only proceeds if tests pass)
FROM build AS publish
WORKDIR /src/WebShop.Api
RUN dotnet publish "WebShop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image for WebShop
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebShop.Api.dll"]