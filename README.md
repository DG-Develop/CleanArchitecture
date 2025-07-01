# ECommerce API (Clean Architecture)

**ECommerceSolution** is a sample ASP.NET Core 8.0 Web API implementing a Clean Architecture for an e-commerce CRUD service.

## 🚀 Tecnologías
- .NET 8.0 SDK
- ASP.NET Core Web API (Minimal & MVC Controllers)
- Entity Framework Core 9.0 (SQL Server)
- MediatR (CQRS)
- FluentValidation
- Docker
- Swagger / OpenAPI

## 📂 Estructura del Proyecto
```
/ (solución)
├─ src/Core/Domain              # Entidades y repositorios (sin dependencias externas)
│  └─ Features/Products
│     └─ Product.cs
│     └─ IProductRepository.cs
├─ src/Core/Application         # Lógica de aplicación (CQRS, validadores)
│  ├─ Features/Products
│  │  ├─ Commands
│  │  ├─ Queries
│  │  └─ Handlers
│  └─ ServiceExtensions.cs      # Registro de MediatR y FluentValidation
├─ src/Infrastructure/Persistence # EF Core DbContext, migraciones y configuraciones
│  ├─ Configuration
│  │  └─ ProductConfiguration.cs
│  ├─ ApplicationDbContext.cs
│  └─ ServiceExtensions.cs      # Registro del DbContext y Repositorios
├─ src/Presentation/WebAPI      # ASP.NET Core Web API (Controllers + DI)
│  ├─ API/Controllers
│  │  └─ ProductsController.cs
│  ├─ Extensions
│  │  └─ ServiceExtensions.cs    # Registro de servicios de infraestructura y aplicación
│  ├─ appsettings.json
│  ├─ Program.cs
│  └─ Dockerfile
└─ .gitignore
```

## 🔧 Requisitos Previos
- .NET 8.0 SDK instalado (https://dotnet.microsoft.com/download)
- SQL Server local o remota
- Docker (opcional para contenedores)

## ⚙️ Configuración
1. Clonar el repositorio:
   ```powershell
   git clone https://github.com/DG-Develop/CleanArchitecture.git
   cd CleanArchitecture
   ```
2. Crear la base de datos de SQL Server `ECommerceDb` o actualizar la cadena de conexión en `src/Presentation/WebAPI/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=ECommerceDb;Trusted_Connection=True;"
   }
   ```
3. Restaurar e iniciar en local:
   ```powershell
   dotnet restore
   dotnet build
   dotnet run --project src/Presentation/WebAPI/ECommerce.API.csproj
   ```
4. Acceder a Swagger en: `https://localhost:5001/swagger/index.html`

## 🐳 Ejecutar con Docker
1. Construir la imagen:
   ```powershell
   docker build -t ecommerce-api .
   ```
2. Ejecutar el contenedor:
   ```powershell
   docker run -d -p 5000:80 --name ecommerce-api ecommerce-api
   ```
3. Swagger disponible en `http://localhost:5000/swagger/index.html`

## 🎯 Principios y Patrones
- **Clean Architecture**: separación en capas independientes.
- **CQRS** con MediatR (Commands y Queries en capas de Application).
- **FluentValidation** para validaciones de entrada.
- **Repository Pattern** para abstracción de datos.
- **DDD**: entidades en capa de Domain.
- **SOLID**: diseño modular, inyección de dependencias.

---

¡Listo para desarrollar y probar tu ECommerce API! 🚀

## Base de datos para las pruebas
https://drive.google.com/drive/folders/105Xy_P82ySQn8Q7_KPpry0bSBor1ly-K?usp=sharing
