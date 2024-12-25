FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./Web/Web.csproj /app/Web/
COPY ./Domain/Domain.csproj /app/Domain/
COPY ./Application/Application.csproj /app/Application/
COPY ./Repository/Repository.csproj /app/Repository/





COPY . ./

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

