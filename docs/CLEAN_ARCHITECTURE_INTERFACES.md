# ¿Cuándo va una interfaz en el Dominio y cuándo en otra capa?

## Principios Fundamentales de Clean Architecture

En una arquitectura limpia, la ubicación de las interfaces sigue el **Principio de Inversión de Dependencias (DIP)** y el principio de que **las dependencias apuntan hacia adentro**.

## Reglas para la Ubicación de Interfaces

### 1. Interfaces en la Capa de Dominio

**¿Cuándo?** Cuando el dominio necesita un servicio o repositorio, pero no debe depender de implementaciones externas.

**Propósito:** Permitir que el dominio defina **QUÉ** necesita sin saber **CÓMO** se implementa.

#### Ejemplos en este proyecto:

```csharp
// ✅ CORRECTO: En Domain/Interfaces/
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> AddAsync(T entidad);
}
```

**¿Por qué en el Dominio?**
- El dominio **posee** estos contratos
- Define la abstracción que necesita para funcionar
- Las implementaciones (Infrastructure) dependen del dominio, no al revés

### 2. Interfaces en la Capa de Application

**¿Cuándo?** Para servicios de aplicación, casos de uso, y contratos específicos de la lógica de negocio de la aplicación.

#### Ejemplos que deberíamos tener:

```csharp
// ✅ En Application/Interfaces/
public interface IProductApplicationService
{
    Task<ProductDto> CreateProductAsync(CreateProductRequest request);
    Task<ProductDto> UpdateProductAsync(UpdateProductRequest request);
    Task<bool> DeleteProductAsync(int id);
}

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
    Task<bool> DeleteFileAsync(string fileName);
}
```

**¿Por qué en Application?**
- Son contratos específicos de casos de uso de la aplicación
- Coordinan múltiples operaciones del dominio
- Manejan flujos de trabajo complejos

### 3. Interfaces en la Capa de Infrastructure

**¿Cuándo?** Para contratos específicos de tecnología o implementaciones externas.

#### Ejemplos:

```csharp
// ✅ En Infrastructure/Interfaces/
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public interface ICacheProvider
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
}
```

## Diagrama de Dependencias

```
┌─────────────────┐
│   Presentation  │──┐
└─────────────────┘  │
                     ▼
┌─────────────────┐  ┌─────────────────┐
│ Infrastructure  │─▶│   Application   │
└─────────────────┘  └─────────────────┘
                              │
                              ▼
                     ┌─────────────────┐
                     │     Domain      │ ◀── Interfaces aquí definen
                     └─────────────────┘     lo que el dominio necesita
```

## Ejemplos Prácticos de este Proyecto

### ✅ Bien Ubicadas (Domain/Interfaces/)

1. **IProductRepository**: El dominio necesita persistir productos
2. **IGenericRepository<T>**: Operaciones CRUD genéricas que el dominio requiere
3. **ICatProductsType**: Operaciones específicas para categorías de productos

### 🔄 Podrían mejorarse

Crear en `Application/Interfaces/`:

```csharp
public interface IProductApplicationService
{
    Task<ProductResultDto> GetProductWithDetailsAsync(int id);
    Task<PaginatedResult<ProductDto>> GetProductsPaginatedAsync(GetProductsRequest request);
}

public interface INotificationService
{
    Task NotifyProductCreatedAsync(int productId);
    Task NotifyLowStockAsync(int productId);
}
```

## Casos Especiales

### MediatR Requests/Handlers

En este proyecto se usa **MediatR** para CQRS:

```csharp
// ✅ En Application/Features/Products/Queries/
public class GetProductByIdQuery : IRequest<ProductDto>
{
    public int Id { get; set; }
}

// ✅ En Application/Features/Products/Handlers/
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    // Implementación...
}
```

**¿Por qué aquí y no interfaces separadas?**
- MediatR ya proporciona las abstracciones (`IRequest`, `IRequestHandler`)
- Los handlers son implementaciones específicas de casos de uso
- La responsabilidad está en Application, no en Domain

## Regla Práctica Simple

1. **¿El dominio lo necesita?** → `Domain/Interfaces/`
2. **¿Es un servicio de aplicación/caso de uso?** → `Application/Interfaces/`
3. **¿Es específico de tecnología?** → `Infrastructure/Interfaces/`
4. **¿Es para coordinación entre capas?** → `Application/Interfaces/`

## Anti-Patrones a Evitar

❌ **Interfaces en Domain que dependen de tecnología específica:**
```csharp
// ❌ MAL: No debería estar en Domain
public interface IEntityFrameworkRepository
{
    DbSet<T> GetDbSet<T>() where T : class;
}
```

❌ **Interfaces en Infrastructure que el Domain necesita:**
```csharp
// ❌ MAL: Si Domain lo necesita, debe estar en Domain
public interface IProductRepository // En Infrastructure
{
    Task<Product> GetByIdAsync(int id);
}
```

## Beneficios de la Correcta Ubicación

1. **Testabilidad**: Fácil creación de mocks/stubs
2. **Independencia**: Domain no depende de detalles externos
3. **Flexibilidad**: Fácil cambio de implementaciones
4. **Claridad**: Cada capa tiene responsabilidades bien definidas