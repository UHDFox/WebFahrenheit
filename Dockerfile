FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./Web/Web.csproj /app/Web/
COPY ./Domain/Domain.csproj /app/Domain/
COPY ./Application/Application.csproj /app/Application/
COPY ./Repository/Repository.csproj /app/Repository/
COPY ../FahrenheitAuthService/FahrenheitAuthService.csproj /app/FahrenheitAuthService

COPY . ./



# Добавляем этот путь как источник NuGet
RUN mkdir -p /app/nuget
RUN dotnet nuget add source /app/nuget --name DockerFahrenheitRepo

RUN dotnet pack /app/FahrenheitAuthService.Client/FahrenheitAuthService.Client.csproj --configuration Release --output /app/nuget /p:PackageVersion=1.0.0

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

