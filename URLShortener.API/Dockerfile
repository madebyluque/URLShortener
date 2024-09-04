FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["URLShortener.API/URLShortener.API.csproj", "URLShortener.API/"]
COPY ["URLShortener.Application/URLShortener.Application.csproj", "URLShortener.Application/"]
COPY ["URLShortener.Domain/URLShortener.Domain.csproj", "URLShortener.Domain/"]
COPY ["URLShortener.Infrastructure/URLShortener.Infrastructure.csproj", "URLShortener.Infrastructure/"]
RUN dotnet restore "./URLShortener.API/URLShortener.API.csproj"
COPY . .
WORKDIR "/src/URLShortener.API"
RUN dotnet build "./URLShortener.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./URLShortener.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "URLShortener.API.dll"]