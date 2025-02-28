# Стадия 1: Сборка и восстановление зависимостей
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Копируем проекты и зависимости
COPY ./Web/Web.csproj /app/Web/
COPY ./Domain/Domain.csproj /app/Domain/
COPY ./Application/Application.csproj /app/Application/
COPY ./Repository/Repository.csproj /app/Repository/

# Копируем NuGet.config
COPY ./NuGet.config /app/NuGet.config

COPY . .

# Восстанавливаем зависимости, используя GitHub Packages
RUN dotnet restore /app/Web/Web.csproj 
RUN dotnet restore /app/Application/Application.csproj
RUN dotnet restore /app/Domain/Domain.csproj
RUN dotnet restore /app/Repository/Repository.csproj 

# Очищаем старые сборки
RUN dotnet clean /app/Application/Application.csproj -c Release
RUN dotnet clean /app/Domain/Domain.csproj -c Release
RUN dotnet clean /app/Repository/Repository.csproj -c Release
RUN dotnet clean /app/Web/Web.csproj -c Release

# Собираем и публикуем веб-приложение
RUN dotnet publish /app/Web/Web.csproj -c Release -o /Release
RUN ls -l /Release



# Стадия 2: Финальный образ для веб-приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Копируем опубликованные файлы веб-приложения
COPY --from=build /Release .

# Точка входа
ENTRYPOINT ["dotnet", "Web.dll"]
