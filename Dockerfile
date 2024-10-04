FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 44352

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

COPY ["CalculoCDB.API/CalculoCDB.API.csproj", "CalculoCDB.API/"]
COPY ["CalculoCDB.ApplicationCore/CalculoCDB.ApplicationCore.csproj", "CalculoCDB.ApplicationCore/"]

RUN dotnet restore "CalculoCDB.API/CalculoCDB.API.csproj"
COPY . .

WORKDIR "CalculoCDB.API"
RUN dotnet build "CalculoCDB.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculoCDB.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculoCDB.API.dll"]
