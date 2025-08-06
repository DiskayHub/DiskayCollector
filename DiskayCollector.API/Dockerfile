FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DiskayCollector.API/DiskayCollector.API.csproj", "DiskayCollector.API/"]
COPY ["DiskayCollector.Core/DiskayCollector.Core.csproj", "DiskayCollector.Core/"]
COPY ["DiskayCollector.Postgres/DiskayCollector.Postgres.csproj", "DiskayCollector.Postgres/"]
RUN dotnet restore "DiskayCollector.API/DiskayCollector.API.csproj"
COPY . .
WORKDIR "/src/DiskayCollector.API"
RUN dotnet build "./DiskayCollector.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DiskayCollector.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiskayCollector.API.dll"]
