@echo off
echo ========================================
echo   Sistema de Facturacion
echo   Iniciando API y Web en HTTPS
echo ========================================
echo.

cd /d "%~dp0"

start "Invoicing API - HTTPS" cmd /k "cd Invoicing.API && echo [API] Iniciando en https://localhost:7065 && dotnet run --launch-profile https"

timeout /t 8 /nobreak

start "Invoicing Web - HTTPS" cmd /k "cd Invoicing.Web && echo [WEB] Iniciando en https://localhost:7057 && dotnet run --launch-profile https"

echo.
echo ========================================
echo   Proyectos iniciados en HTTPS!
echo   API:  https://localhost:7065/swagger
echo   Web:  https://localhost:7057
echo ========================================
echo.
echo Presiona cualquier tecla para salir...
pause > nul
