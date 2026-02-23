# Sistema de Facturación - Prueba Técnica Novasoft

Sistema completo de gestión de facturación con integración a servicios externos (API Novasoft) desarrollado en .NET 10 con arquitectura limpia.

## 📋 Tabla de Contenidos

- [Características](#características)
- [Arquitectura](#arquitectura)
- [Tecnologías](#tecnologías)
- [Base de Datos](#base-de-datos)
- [Requisitos Previos](#requisitos-previos)
- [Instalación y Ejecución](#instalación-y-ejecución)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [API Endpoints](#api-endpoints)

---

## 🎯 Características

### Gestión de Facturas
- ✅ Creación de facturas con múltiples artículos
- ✅ Cálculo automático de IVA (19%)
- ✅ Aplicación de descuentos (5% sobre $500,000)
- ✅ Listado completo de facturas
- ✅ Detalle de factura con información completa

### Gestión de Cuentas (Integración Novasoft)
- ✅ Autenticación automática con API externa
- ✅ Creación de cuentas en servicio externo
- ✅ Listado de cuentas desde servicio externo
- ✅ Manejo robusto de errores y timeouts

### Frontend Web
- ✅ Interfaz moderna con Tailwind CSS
- ✅ Diseño responsive (móvil, tablet, desktop)
- ✅ Formularios dinámicos con validación
- ✅ Cálculo de totales en tiempo real
- ✅ Mensajes de error/éxito informativos

---

## 🏗️ Arquitectura

El proyecto sigue **Clean Architecture** con separación clara de responsabilidades:

```
┌─────────────────────────────────────────────────┐
│                  Presentación                   │
│  ┌──────────────┐         ┌─────────────────┐  │
│  │ Invoicing.API│         │ Invoicing.Web   │  │
│  │ (REST API)   │         │ (Razor Pages)   │  │
│  └──────────────┘         └─────────────────┘  │
└─────────────────────────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────┐
│               Capa de Aplicación                │
│           (Invoicing.Application)               │
│  ┌────────────┐  ┌──────────┐  ┌────────────┐  │
│  │  Commands  │  │  Queries │  │ Validators │  │
│  │  (CQRS)    │  │  (CQRS)  │  │ (FluentVal)│  │
│  └────────────┘  └──────────┘  └────────────┘  │
│        MediatR + AutoMapper + FluentValidation  │
└─────────────────────────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────┐
│               Capa de Dominio                   │
│            (Invoicing.Domain)                   │
│  ┌────────────┐  ┌──────────┐  ┌────────────┐  │
│  │ Entities   │  │  Value   │  │ Interfaces │  │
│  │            │  │  Objects │  │            │  │
│  └────────────┘  └──────────┘  └────────────┘  │
│         Lógica de negocio pura (sin deps)       │
└─────────────────────────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────┐
│            Capa de Infraestructura              │
│          (Invoicing.Infrastructure)             │
│  ┌────────────┐  ┌──────────┐  ┌────────────┐  │
│  │ Persistence│  │ External │  │  Services  │  │
│  │ (EF Core)  │  │   APIs   │  │            │  │
│  └────────────┘  └──────────┘  └────────────┘  │
│    SQL Server + HttpClient + Implementaciones   │
└─────────────────────────────────────────────────┘
```

### Patrones Implementados

#### **CQRS (Command Query Responsibility Segregation)**
- **Commands**: Operaciones que modifican estado (CreateInvoice, CreateAccount)
- **Queries**: Operaciones de solo lectura (GetInvoices, GetAccounts)
- **Mediador**: MediatR para desacoplar handlers

#### **Repository Pattern**
- Abstracción de acceso a datos
- Facilita testing y cambio de tecnología de persistencia

#### **Dependency Injection**
- Inversión de control en toda la aplicación
- Registro de servicios en `DependencyInjection.cs`

#### **Validation Pipeline**
- FluentValidation para reglas de negocio
- Pipeline behavior para validación automática

#### **Exception Handling**
- Middleware centralizado para manejo de excepciones
- Excepciones tipadas (DomainException, InfrastructureException)
- Códigos de error estructurados

---

## 🛠️ Tecnologías

### Backend
- **.NET 10** - Framework principal
- **ASP.NET Core** - API REST
- **Entity Framework Core 10** - ORM
- **MediatR** - Patrón Mediador para CQRS
- **FluentValidation** - Validaciones
- **AutoMapper** - Mapeo objeto-objeto
- **Swagger/OpenAPI** - Documentación API

### Frontend
- **Razor Pages** - Framework web
- **Tailwind CSS** - Estilos y diseño
- **JavaScript Vanilla** - Interactividad

### Base de Datos
- **SQL Server LocalDB** - Almacenamiento local

### APIs Externas
- **Novasoft API** - Gestión de cuentas externas

---

## 💾 Base de Datos

### SQL Server LocalDB

La aplicación utiliza **SQL Server LocalDB**, una versión ligera de SQL Server ideal para desarrollo.

#### **Configuración**

**Connection String** (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoicingDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### **Ubicación de Archivos**

Los archivos físicos de la base de datos se encuentran en:
```
C:\Users\<TuUsuario>\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\
```

O pueden estar en:
```
C:\Users\<TuUsuario>\
```

**Archivos generados:**
- `InvoicingDb.mdf` - Archivo de datos principal
- `InvoicingDb_log.ldf` - Log de transacciones

#### **Migraciones Automáticas**

La aplicación ejecuta las migraciones automáticamente al iniciar:

```csharp
// En Program.cs
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InvoicingDbContext>();
    context.Database.Migrate(); // Crea/actualiza la BD automáticamente
}
```

#### **Esquema de Base de Datos**

**Tabla: Invoices**
```sql
CREATE TABLE Invoices (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Date DATETIME NOT NULL,
    ClientDocNumber VARCHAR(15) NOT NULL,
    ClientFirstName NVARCHAR(MAX) NOT NULL,
    ClientLastName NVARCHAR(MAX) NOT NULL,
    ClientAddress NVARCHAR(MAX) NOT NULL,
    ClientPhone NVARCHAR(MAX) NOT NULL,
    GrossValue DECIMAL(18,2) NOT NULL,
    Discount DECIMAL(18,2) NOT NULL,
    Tax DECIMAL(18,2) NOT NULL,
    TotalValue DECIMAL(18,2) NOT NULL
)
```

**Tabla: InvoiceItems**
```sql
CREATE TABLE InvoiceItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,
    ArticleCode INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id) ON DELETE CASCADE
)
```

#### **Gestión de la Base de Datos**

**Ver datos en Visual Studio:**
1. **View** → **SQL Server Object Explorer**
2. Expandir **(localdb)\MSSQLLocalDB**
3. Expandir **Databases** → **InvoicingDb**
4. Clic derecho en una tabla → **View Data**

**Consultas SQL directas:**
```sql
-- Ver todas las facturas
SELECT * FROM Invoices ORDER BY Date DESC;

-- Ver items de una factura específica
SELECT * FROM InvoiceItems WHERE InvoiceId = 1;

-- Ver factura completa con items
SELECT i.*, it.*
FROM Invoices i
LEFT JOIN InvoiceItems it ON i.Id = it.InvoiceId
WHERE i.Id = 1;
```

**Resetear la base de datos:**
```sql
DROP DATABASE InvoicingDb;
-- Reinicia la aplicación para que se cree nuevamente
```

---

## 📦 Requisitos Previos

### Software Necesario

1. **.NET 10 SDK**
   - Descargar: https://dotnet.microsoft.com/download/dotnet/10.0
   - Verificar instalación: `dotnet --version`

2. **SQL Server LocalDB** (incluido con Visual Studio)
   - O descargar: https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb

3. **Visual Studio 2022** (recomendado)
   - O Visual Studio Code con extensiones de C#

4. **Node.js** (para compilar Tailwind CSS - opcional)
   - Descargar: https://nodejs.org/
   - Solo necesario si quieres modificar estilos

### Verificar Instalación

```powershell
# .NET
dotnet --version
# Debe mostrar: 10.0.x

# SQL Server LocalDB
sqllocaldb info
# Debe mostrar: MSSQLLocalDB

# Node.js (opcional)
node --version
npm --version
```

---

## 🚀 Instalación y Ejecución

### Método 1: Script Automático (Recomendado)

**Windows:**
```cmd
start-https.bat
```

**Linux/Mac:**
```bash
# Dar permisos de ejecución (solo la primera vez)
chmod +x start-https.sh

# Ejecutar
./start-https.sh
```

Este script:
1. Inicia la API en `https://localhost:7065`
2. Espera 8 segundos
3. Inicia el Web en `https://localhost:7057`
4. Windows: Abre dos ventanas de terminal
5. Linux/Mac: Ejecuta en segundo plano (Ctrl+C para detener)

### Método 2: Visual Studio

1. **Configurar Multiple Startup Projects:**
   - Clic derecho en la **Solución**
   - **"Set Startup Projects..."**
   - Seleccionar **"Multiple startup projects"**
   - Configurar:
     - `Invoicing.API` → **Start**
     - `Invoicing.Web` → **Start**
   - **OK**

2. **Ejecutar:**
   - Presionar **F5** o clic en **"Start"**

### Método 3: Terminal Manual

**Terminal 1 - API:**
```powershell
cd Invoicing.API
dotnet run --launch-profile https
```

**Terminal 2 - Web:**
```powershell
cd Invoicing.Web
dotnet run --launch-profile https
```

### Acceder a la Aplicación

Después de iniciar:

- **📡 API (Swagger)**: https://localhost:7065/swagger
- **🌐 Aplicación Web**: https://localhost:7057

### Compilar Tailwind CSS (Opcional)

Si modificas estilos:

```bash
cd Invoicing.Web

# Instalar dependencias
npm install

# Compilar CSS una vez
npm run build:css

# O compilar automáticamente al hacer cambios
npm run watch:css
```

---

## 📁 Estructura del Proyecto

```
📦 Invoicing Solution
├── 📂 Invoicing.API                 # API REST (Presentación)
│   ├── Controllers/                 # Controladores API
│   │   ├── InvoicesController.cs    # Endpoints de facturas
│   │   └── AccountsController.cs    # Endpoints de cuentas
│   ├── Middleware/                  # Middleware personalizado
│   │   └── ExceptionHandlingMiddleware.cs
│   ├── Properties/
│   │   └── launchSettings.json      # Configuración de puertos
│   ├── appsettings.json             # Configuración (DB, APIs externas)
│   └── Program.cs                   # Punto de entrada, CORS, Swagger
│
├── 📂 Invoicing.Application         # Lógica de Aplicación (CQRS)
│   ├── Invoices/
│   │   ├── Commands/                # Comandos (escritura)
│   │   │   └── CreateInvoice/
│   │   └── Queries/                 # Consultas (lectura)
│   │       ├── GetInvoicesList/
│   │       └── GetInvoiceById/
│   ├── Accounts/
│   │   ├── Commands/
│   │   │   └── CreateAccount/
│   │   └── Queries/
│   │       └── GetAccountsList/
│   ├── Common/
│   │   ├── Behaviors/               # Pipelines (validación)
│   │   └── Mappings/                # AutoMapper profiles
│   └── DependencyInjection.cs       # Registro de servicios
│
├── 📂 Invoicing.Domain              # Dominio (Entidades, Lógica)
│   ├── Entities/                    # Entidades de negocio
│   │   ├── Invoice.cs
│   │   └── InvoiceItem.cs
│   ├── ValueObjects/                # Objetos de valor
│   │   └── ExternalAccount.cs
│   ├── Interfaces/                  # Contratos
│   │   ├── IInvoiceRepository.cs
│   │   └── IAccountService.cs
│   ├── Exceptions/                  # Excepciones de dominio
│   │   ├── DomainException.cs
│   │   └── DomainError.cs
│   └── Constants/                   # Constantes de negocio
│       └── AccountConstants.cs
│
├── 📂 Invoicing.Infrastructure      # Infraestructura (Persistencia, APIs)
│   ├── Persistence/
│   │   ├── Context/
│   │   │   └── InvoicingDbContext.cs     # EF Core DbContext
│   │   ├── Repositories/
│   │   │   └── InvoiceRepository.cs
│   │   └── Migrations/                   # Migraciones EF Core
│   ├── Services/
│   │   └── NovasoftAccountService.cs     # Cliente API Novasoft
│   ├── ExternalModels/              # DTOs de APIs externas
│   │   ├── NovasoftLoginRequest.cs
│   │   ├── NovasoftAccountRequest.cs
│   │   └── NovasoftAccountResponse.cs
│   ├── Common/
│   │   └── Mappings/                # AutoMapper (externo → dominio)
│   ├── Exceptions/
│   │   └── InfrastructureException.cs
│   └── DependencyInjection.cs       # Registro de servicios
│
└── 📂 Invoicing.Web                 # Frontend (Razor Pages)
    ├── Pages/
    │   ├── Invoices/                # Páginas de facturas
    │   │   ├── Create.cshtml/.cs    # Crear factura
    │   │   ├── List.cshtml/.cs      # Listar facturas
    │   │   └── Detail.cshtml/.cs    # Detalle de factura
    │   ├── Accounts/                # Páginas de cuentas
    │   │   ├── Create.cshtml/.cs    # Crear cuenta
    │   │   └── List.cshtml/.cs      # Listar cuentas
    │   ├── Shared/
    │   │   └── _Layout.cshtml       # Layout principal
    │   └── Index.cshtml             # Página de inicio
    ├── Services/                    # Servicios HTTP
    │   ├── InvoiceService.cs        # Cliente para API de facturas
    │   └── AccountService.cs        # Cliente para API de cuentas
    ├── Models/                      # ViewModels
    │   ├── InvoiceModels.cs
    │   └── AccountModels.cs
    ├── wwwroot/
    │   └── css/
    │       ├── site.css             # Tailwind source
    │       └── output.css           # Tailwind compilado
    ├── tailwind.config.js           # Configuración Tailwind
    ├── package.json                 # Dependencias Node.js
    ├── appsettings.json             # URL de la API
    └── Program.cs                   # Configuración de servicios HTTP
```

---

## ✨ Funcionalidades

### 1. Gestión de Facturas

#### Crear Factura
- **Endpoint**: `POST /api/invoices`
- **Frontend**: `/Invoices/Create`

**Características:**
- Captura de datos del cliente (documento, nombre, dirección, teléfono)
- Agregar múltiples artículos dinámicamente
- Cálculo automático en tiempo real de:
  - Valor bruto (suma de items)
  - Descuento (5% si bruto >= $500,000)
  - IVA (19% sobre valor neto)
  - Total final
- Validaciones de campos obligatorios
- Reindexación automática de items al eliminar

#### Listar Facturas
- **Endpoint**: `GET /api/invoices`
- **Frontend**: `/Invoices/List`

**Características:**
- Tabla responsive con todas las facturas
- Estadísticas: total facturas, total facturado, promedio
- Ordenamiento por fecha descendente
- Formato de moneda colombiana (COP)
- Enlace a detalle de cada factura

#### Ver Detalle
- **Endpoint**: `GET /api/invoices/{id}`
- **Frontend**: `/Invoices/Detail/{id}`

**Características:**
- Información completa del cliente
- Tabla de artículos con totales
- Desglose de cálculos (bruto, descuento, IVA, total)

### 2. Gestión de Cuentas (Novasoft)

#### Crear Cuenta
- **Endpoint**: `POST /api/accounts`
- **Frontend**: `/Accounts/Create`

**Características:**
- Autenticación automática con API Novasoft
- Formulario completo con todos los campos requeridos
- Valores predeterminados para campos constantes
- Manejo de errores de autenticación y validación

#### Listar Cuentas
- **Endpoint**: `GET /api/accounts`
- **Frontend**: `/Accounts/List`

**Características:**
- Grid de cards responsive
- Información completa de cada cuenta
- Estadística de total de cuentas
- Integración en tiempo real con API externa

### 3. Reglas de Negocio

**Facturación:**
- IVA: 19% sobre valor neto (después de descuento)
- Descuento: 5% si valor bruto >= $500,000
- Validación: Mínimo 1 artículo por factura
- Validación: Todos los campos obligatorios

**Cuentas:**
- Ciudad por defecto: 11001 (Bogotá)
- Departamento: 11 (Bogotá D.C.)
- País: 057 (Colombia)
- Tipo de cliente: 2
- Tipo de persona: 2
- Estado: 1 (Activo)

---

## 🔌 API Endpoints

### Facturas

#### Crear Factura
```http
POST /api/invoices
Content-Type: application/json

{
  "docNumber": "1006875365",
  "firstName": "Jorge",
  "lastName": "Abella",
  "address": "Calle 123 #45-67",
  "phone": "3001234567",
  "items": [
    {
      "articleCode": 101,
      "quantity": 2,
      "unitPrice": 250000
    }
  ]
}

Response: 200 OK
{
  "id": 1
}
```

#### Listar Facturas
```http
GET /api/invoices

Response: 200 OK
[
  {
    "id": 1,
    "date": "2024-01-15T10:30:00",
    "clientName": "Jorge Abella",
    "clientDocNumber": "1006875365",
    "grossValue": 500000,
    "discount": 25000,
    "tax": 90250,
    "total": 565250
  }
]
```

#### Obtener Detalle
```http
GET /api/invoices/1

Response: 200 OK
{
  "id": 1,
  "date": "2024-01-15T10:30:00",
  "client": {
    "docNumber": "1006875365",
    "firstName": "Jorge",
    "lastName": "Abella",
    "address": "Calle 123 #45-67",
    "phone": "3001234567"
  },
  "items": [
    {
      "id": 1,
      "articleCode": 101,
      "quantity": 2,
      "unitPrice": 250000,
      "total": 500000
    }
  ],
  "grossValue": 500000,
  "discount": 25000,
  "tax": 90250,
  "totalValue": 565250
}
```

### Cuentas

#### Crear Cuenta
```http
POST /api/accounts
Content-Type: application/json

{
  "code": "456",
  "name": "Empresa XYZ S.A.S",
  "identification": "900123456",
  "email": "contacto@empresa.com",
  "address": "Calle 100 #50-25",
  "phone": "6012345678",
  "lastName": "García",
  "firstName": "Juan",
  "externalClientCode": "EXT001",
  "webPage": "https://empresa.com"
}

Response: 200 OK
{
  "success": true
}
```

#### Listar Cuentas
```http
GET /api/accounts

Response: 200 OK
[
  {
    "clientCode": "456",
    "name": "Empresa XYZ S.A.S",
    "identification": "900123456",
    "email": "contacto@empresa.com",
    "address": "Calle 100 #50-25",
    "phone": "6012345678",
    "firstName": "Juan",
    "lastName": "García"
  }
]
```

---

## 🎨 Frontend - Descripción General

### Tecnología
- **Razor Pages** - Framework web de .NET
- **Tailwind CSS 3.4** - Framework de estilos utility-first
- **JavaScript Vanilla** - Sin frameworks adicionales

### Arquitectura del Frontend

```
┌─────────────────────────────────────────┐
│         Navegador del Usuario           │
└─────────────────────────────────────────┘
              │
              ↓
┌─────────────────────────────────────────┐
│        Razor Pages (.cshtml)            │
│  - Renderizado en servidor              │
│  - HTML dinámico con C#                 │
│  - Validación con Tag Helpers           │
└─────────────────────────────────────────┘
              │
              ↓
┌─────────────────────────────────────────┐
│        Page Models (.cshtml.cs)         │
│  - Lógica de presentación               │
│  - Binding de modelos                   │
│  - Llamadas a servicios HTTP            │
└─────────────────────────────────────────┘
              │
              ↓
┌─────────────────────────────────────────┐
│      Servicios HTTP (Services/)         │
│  - InvoiceService.cs                    │
│  - AccountService.cs                    │
│  - HttpClient para consumir API         │
└─────────────────────────────────────────┘
              │
              ↓
┌─────────────────────────────────────────┐
│         API REST (Backend)              │
│      https://localhost:7065/api/        │
└─────────────────────────────────────────┘
```

### Características Principales

#### **1. Layout Responsive (_Layout.cshtml)**
- Navbar con navegación completa
- Diseño mobile-first
- Footer consistente
- Iconos SVG inline

#### **2. Páginas Dinámicas**

**Crear Factura:**
- Formulario con múltiples secciones
- Agregar/eliminar items dinámicamente
- Cálculo de totales en tiempo real
- Validación client-side y server-side

**JavaScript Key Features:**
```javascript
// Reindexación automática de items
function reindexItems() {
    const rows = document.querySelectorAll('.item-row');
    rows.forEach((row, index) => {
        row.querySelectorAll('input').forEach(input => {
            const name = input.getAttribute('name');
            if (name) {
                input.setAttribute('name', name.replace(/\[\d+\]/, `[${index}]`));
            }
        });
    });
}

// Cálculo de totales
function calculateTotals() {
    let gross = 0;
    document.querySelectorAll('.item-row').forEach(row => {
        const quantity = parseFloat(row.querySelector('.item-quantity').value) || 0;
        const price = parseFloat(row.querySelector('.item-price').value) || 0;
        gross += quantity * price;
    });
    
    const discount = gross >= 500000 ? gross * 0.05 : 0;
    const taxBase = gross - discount;
    const tax = taxBase * 0.19;
    const total = taxBase + tax;
    
    // Actualizar UI...
}
```

**Listar Facturas:**
- Tabla responsive con todas las facturas
- Cards de estadísticas (total, suma, promedio)
- Enlaces a detalle

**Crear Cuenta:**
- Formulario completo para API Novasoft
- Campos predeterminados ocultos
- Validación en tiempo real

#### **3. Tailwind CSS**

**Clases Personalizadas (site.css):**
```css
@layer components {
    .btn-primary {
        @apply bg-blue-600 hover:bg-blue-700 text-white 
               font-semibold py-2 px-4 rounded-lg shadow-md 
               transition duration-200;
    }
    
    .input-field {
        @apply w-full px-3 py-2 border border-gray-300 rounded-lg 
               focus:outline-none focus:ring-2 focus:ring-blue-500;
    }
    
    .label-text {
        @apply block text-sm font-medium text-gray-700 mb-1;
    }
}
```

**Compilación:**
```bash
# Desarrollo (watch mode)
npm run watch:css

# Producción (minificado)
npm run build:css
```

#### **4. Servicios HTTP**

**InvoiceService.cs:**
```csharp
public class InvoiceService
{
    private readonly HttpClient _httpClient;
    
    public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
    {
        var invoices = await _httpClient.GetFromJsonAsync<List<InvoiceDTO>>("api/invoices");
        return invoices ?? new List<InvoiceDTO>();
    }
    
    public async Task<int> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/invoices", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }
}
```

**Configuración en Program.cs:**
```csharp
builder.Services.AddHttpClient<InvoiceService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7065");
});
```

#### **5. Manejo de Errores**

- Try-catch en servicios HTTP
- Mensajes de error amigables
- Validación con FluentValidation en backend
- Validación HTML5 en frontend

---

## 🔒 Configuración de CORS

La API tiene CORS habilitado para permitir peticiones desde el frontend:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
    {
        policy.WithOrigins("https://localhost:7057", "http://localhost:5163")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

app.UseCors("AllowWebApp");
```

---

## 📝 Configuración

### API Settings (Invoicing.API\appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoicingDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "NovasoftApi": {
    "BaseUrl": "https://test.novasoft.com.co:8091/WebAPI/api/",
    "ConnectionName": "DataPower",
    "User": "pruebaTecnica",
    "Password": "P@ssw0rd"
  }
}
```

### Web Settings (Invoicing.Web\appsettings.json)

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7065"
  }
}
```

---

## 🧪 Testing

### Swagger UI
Accede a `https://localhost:7065/swagger` para:
- Probar todos los endpoints
- Ver documentación interactiva
- Ejecutar requests de prueba

### Datos de Prueba

**Factura de prueba:**
```json
{
  "docNumber": "1006875365",
  "firstName": "Jorge",
  "lastName": "Abella",
  "address": "Calle 123 #45-67",
  "phone": "3001234567",
  "items": [
    {
      "articleCode": 101,
      "quantity": 2,
      "unitPrice": 250000
    }
  ]
}
```

---

## 🐛 Solución de Problemas

### Error: "No se puede establecer conexión"
- **Causa**: API no está ejecutándose
- **Solución**: Ejecuta `start-https.bat` o configura Multiple Startup Projects

### Error: "CORS blocked"
- **Causa**: CORS no configurado o puerto incorrecto
- **Solución**: Verifica que CORS esté habilitado en Program.cs

### Error: "Database migration failed"
- **Causa**: SQL Server LocalDB no instalado
- **Solución**: Instala LocalDB o usa SQL Server completo

### Estilos no se cargan
- **Causa**: Tailwind CSS no compilado
- **Solución**: Ejecuta `npm run build:css` en Invoicing.Web

---

## 📧 Contacto

Proyecto desarrollado como prueba técnica para Novasoft.

---

## 📄 Licencia

Este proyecto es de uso educativo/evaluativo.
