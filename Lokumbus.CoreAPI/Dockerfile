# Basis-Image für Laufzeit
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Build-Image mit SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiere die csproj-Datei (Dockerfile und csproj sind im selben Ordner)
COPY ["Lokumbus.CoreAPI.csproj", "."]
RUN dotnet restore "Lokumbus.CoreAPI.csproj"

# Kopiere den restlichen Quellcode und führe den Build durch
COPY . .
RUN dotnet build "Lokumbus.CoreAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish-Step
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lokumbus.CoreAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Finales Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lokumbus.CoreAPI.dll"]