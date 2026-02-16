# Comparación Práctica: Interfaces en Domain vs Application vs Infrastructure

## Análisis del Proyecto Actual

### ✅ Interfaces Correctamente Ubicadas en Domain

```csharp
// src/Core/Domain/Interfaces/IProductRepository.cs
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> AddAsync(Product product);
    Task UpdateAsync(Product product);
    // ...
}
```

**¿Por qué está bien aquí?**
- El **dominio necesita** persistir productos
- Define **qué** operaciones necesita sin importar **cómo** se implementan
- La implementación en Infrastructure depende de esta abstracción

```csharp
// src/Core/Domain/Interfaces/IGenericRepository.cs
public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(int id);
    // ...
}
```

**¿Por qué está bien aquí?**
- Abstracción fundamental que el dominio requiere
- Operaciones CRUD básicas que cualquier entidad puede necesitar
- Siguiendo el patrón Repository correctamente

## 🆕 Nuevas Interfaces Agregadas para Demostrar Diferencias

### Application Layer Interfaces

```csharp
// src/Core/Application/Interfaces/IProductApplicationService.cs
public interface IProductApplicationService
{
    Task<ProductResultDTO> CreateProductWithNotificationAsync(CreateProductRequest request);
    Task<ProductResultDTO> UpdateProductWithCacheInvalidationAsync(UpdateProductRequest request);
    Task<bool> SoftDeleteProductAsync(int productId, string deletedByUser);
}
```

**¿Por qué en Application?**
- Coordina **múltiples operaciones del dominio**
- Maneja **flujos de trabajo complejos** (crear + notificar + cachear)
- Incluye **lógica de aplicación** no lógica de dominio

```csharp
// src/Core/Application/Interfaces/INotificationService.cs
public interface INotificationService
{
    Task SendProductCreatedNotificationAsync(int productId, string recipientEmail);
    Task SendLowStockAlertAsync(int productId, int currentStock, string recipientEmail);
}
```

**¿Por qué en Application?**
- **No es requerido por el dominio** directamente
- Es una **preocupación de la aplicación** (notificar usuarios)
- Coordina **servicios externos**

### Infrastructure Layer Interfaces

```csharp
// src/Infrastructure/Interfaces/IDbConnectionFactory.cs
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
    IDbConnection CreateConnection(string connectionString);
}
```

**¿Por qué en Infrastructure?**
- **Específico de tecnología** (SQL Server, MySQL, etc.)
- Maneja **detalles de bajo nivel**
- No es necesario para Domain ni Application directamente

## Comparación Lado a Lado

| Aspecto | Domain Interfaces | Application Interfaces | Infrastructure Interfaces |
|---------|-------------------|------------------------|---------------------------|
| **Propósito** | Define lo que el dominio necesita | Coordina flujos de trabajo | Abstrae tecnologías específicas |
| **Dependencias** | Infrastructure → Domain | Presentation → Application | Ninguna (o hacia capas externas) |
| **Ejemplo** | `IProductRepository` | `INotificationService` | `IDbConnectionFactory` |
| **Cambia cuando** | Cambian reglas de negocio | Cambian casos de uso | Cambia tecnología |
| **Testabilidad** | Mock para tests de dominio | Mock para tests de aplicación | Mock para tests de infraestructura |

## Ejemplo Práctico de Uso

### En un Handler de MediatR (Application Layer)

```csharp
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    // ✅ Domain interface - el handler necesita persistir
    private readonly IProductRepository _productRepository;
    
    // ✅ Application interface - el handler coordina notificaciones
    private readonly INotificationService _notificationService;
    
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Operación de dominio
        var product = new Product { /* ... */ };
        await _productRepository.AddAsync(product);
        
        // 2. Coordinación de aplicación
        await _notificationService.SendProductCreatedNotificationAsync(product.Id, "admin@test.com");
        
        return new ProductDto { /* ... */ };
    }
}
```

### En una implementación de Infrastructure

```csharp
public class EmailNotificationService : INotificationService
{
    // ✅ Infrastructure interface - implementación usa tecnología específica
    private readonly IEmailProvider _emailProvider;
    
    public async Task SendProductCreatedNotificationAsync(int productId, string recipientEmail)
    {
        // Usa el proveedor específico de email (SendGrid, SMTP, etc.)
        await _emailProvider.SendEmailAsync(
            recipientEmail, 
            "Producto Creado", 
            $"Se creó el producto con ID: {productId}");
    }
}
```

## Decisiones de Diseño

### ❓ ¿Dónde poner IMapper o AutoMapper?

```csharp
// ✅ Application - es coordinación entre capas
public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource source);
}
```

### ❓ ¿Dónde poner validadores?

```csharp
// ✅ Application - son reglas de casos de uso específicos
public interface IValidator<T>
{
    Task<ValidationResult> ValidateAsync(T instance);
}
```

### ❓ ¿Dónde poner servicios de dominio?

```csharp
// ✅ Domain - si contienen lógica de negocio pura
public interface IPriceCalculatorService
{
    decimal CalculateFinalPrice(Product product, Customer customer);
}
```

## Regla de Oro 🌟

1. **¿Lo necesita el dominio para su lógica de negocio?** → `Domain/Interfaces/`
2. **¿Coordina múltiples operaciones o servicios externos?** → `Application/Interfaces/`
3. **¿Es específico de una tecnología o infraestructura?** → `Infrastructure/Interfaces/`

## Beneficios de la Correcta Separación

- ✅ **Testabilidad mejorada**: Cada capa se puede testear independientemente
- ✅ **Flexibilidad**: Fácil cambio de implementaciones sin afectar otras capas
- ✅ **Claridad de responsabilidades**: Cada interfaz tiene un propósito claro
- ✅ **Mantenibilidad**: Cambios en una capa no afectan otras innecesariamente