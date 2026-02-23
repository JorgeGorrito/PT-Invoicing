#!/bin/bash

echo "========================================"
echo "  Sistema de Facturación"
echo "  Iniciando API y Web en HTTPS"
echo "========================================"
echo ""

# Colores para mejor visualización
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # Sin color

# Obtener el directorio del script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Iniciar API en segundo plano
echo -e "${BLUE}[API]${NC} Iniciando en https://localhost:7065"
cd "$SCRIPT_DIR/Invoicing.API"
dotnet run --launch-profile https &
API_PID=$!

# Esperar 8 segundos para que la API inicie
sleep 8

# Iniciar Web en segundo plano
echo -e "${GREEN}[WEB]${NC} Iniciando en https://localhost:7057"
cd "$SCRIPT_DIR/Invoicing.Web"
dotnet run --launch-profile https &
WEB_PID=$!

echo ""
echo "========================================"
echo -e "  Proyectos iniciados en HTTPS!"
echo -e "  ${BLUE}API:${NC}  https://localhost:7065/swagger"
echo -e "  ${GREEN}Web:${NC}  https://localhost:7057"
echo "========================================"
echo ""
echo "Presiona Ctrl+C para detener ambos proyectos..."
echo ""

# Función para manejar Ctrl+C
cleanup() {
    echo ""
    echo "Deteniendo proyectos..."
    kill $API_PID 2>/dev/null
    kill $WEB_PID 2>/dev/null
    echo "Proyectos detenidos."
    exit 0
}

# Capturar Ctrl+C
trap cleanup SIGINT SIGTERM

# Esperar a que ambos procesos terminen
wait $API_PID $WEB_PID
