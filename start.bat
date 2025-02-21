@echo off

docker network inspect fahrenheit_network >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo Creating network fahrenheit_network...
    docker network create fahrenheit_network
) else (
    echo Network fahrenheit_network already exists.
    echo Network fahrenheit_network is running now.
)

docker compose -f docker-compose-backend.yaml up --build