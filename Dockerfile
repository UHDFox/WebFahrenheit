FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./WebFahrenheit/Web/Web.csproj /app/Web/
COPY ./WebFahrenheit/Domain/Domain.csproj /app/Domain/
COPY ./WebFahrenheit/Application/Application.csproj /app/Application/
COPY ./WebFahrenheit/Repository/Repository.csproj /app/Repository/
COPY ./FahrenheitAuthService/src/Web/Web.csproj /app/FahrenheitAuthService/src/Web/Web.csproj


COPY ./WebFahrenheit ./
COPY ./FahrenheitAuthService /app/FahrenheitAuthService



# Добавляем этот путь как источник NuGet
RUN mkdir -p /app/nuget
RUN dotnet nuget add source /app/nuget --name DockerFahrenheitRepo

RUN dotnet pack /app/FahrenheitAuthService/FahrenheitAuthService.Client/FahrenheitAuthService.Client.csproj --configuration Release --output /app/nuget /p:PackageVersion=1.0.0
RUN dotnet pack /app/FahrenheitAuthService/src/Contracts/Contracts.csproj --configuration Release --output /app/nuget /p:PackageVersion=1.0.0
RUN dotnet restore

RUN dotnet clean /app/Application/Application.csproj -c Release
RUN dotnet clean /app/Domain/Domain.csproj -c Release
RUN dotnet clean /app/Repository/Repository.csproj -c Release
RUN dotnet clean /app/Web/Web.csproj -c Release



RUN dotnet publish /app/Web/Web.csproj -c Release -o /Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /Release .

ENTRYPOINT ["dotnet", "Web.dll"]

