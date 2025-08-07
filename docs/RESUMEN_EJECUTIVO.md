# Resumen Ejecutivo: Ubicación de Interfaces en Clean Architecture

## Pregunta Original
**"En una arquitectura limpia cuando va una interfaz en el dominio y cuando en interfaz?"**

## Respuesta Rápida

### 🎯 Regla de Oro
**La ubicación de una interfaz depende de QUIÉN la POSEE y PARA QUÉ se usa:**

| Ubicación | ¿Cuándo? | Ejemplo | ¿Por qué? |
|-----------|----------|---------|-----------|
| **Domain/Interfaces/** | El dominio la necesita para funcionar | `IProductRepository` | El dominio define QUÉ necesita |
| **Application/Interfaces/** | Coordina casos de uso complejos | `INotificationService` | La aplicación coordina workflows |
| **Infrastructure/Interfaces/** | Específico de tecnología | `IDbConnectionFactory` | Abstrae detalles técnicos |

## Ejemplos Prácticos del Proyecto

### ✅ Correctamente en Domain
```csharp
// Domain "posee" este contrato
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> AddAsync(Product product);
}
```

### ✅ Nuevamente Agregado en Application
```csharp
// Application coordina múltiples operaciones
public interface IProductApplicationService
{
    Task<ProductResultDTO> CreateProductWithNotificationAsync(CreateProductRequest request);
    Task<bool> SoftDeleteProductAsync(int productId, string deletedByUser);
}
```

### ✅ Nuevamente Agregado en Infrastructure
```csharp
// Específico de tecnología
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
```

## Análisis del Proyecto Actual

### Lo que ya está bien 👍
- `IProductRepository` en Domain ✅
- `IGenericRepository<T>` en Domain ✅
- Uso de MediatR para CQRS ✅

### Lo que se mejoró 🚀
- ✅ Agregado `Application/Interfaces/` con ejemplos prácticos
- ✅ Agregado `Infrastructure/Interfaces/` con ejemplos técnicos
- ✅ Documentación completa con ejemplos del proyecto real
- ✅ Servicio de aplicación de ejemplo mostrando coordinación

## Principios Aplicados

### 1. Dependency Inversion Principle (DIP)
```
Infrastructure → Application → Domain
     ↑              ↑           ↑
Implementa    Coordina    Define contratos
```

### 2. Separation of Concerns
- **Domain**: Lógica de negocio pura
- **Application**: Casos de uso y coordinación
- **Infrastructure**: Detalles técnicos

### 3. Testability
Cada capa puede ser testeada independientemente con mocks/stubs.

## Casos Especiales Explicados

### MediatR (IRequest/IRequestHandler)
```csharp
// ✅ En Application - MediatR ya proporciona las abstracciones
public class GetProductByIdQuery : IRequest<ProductDto> { }
```

### Domain Services
```csharp
// ✅ En Domain - lógica de negocio que no pertenece a entidades
public interface IPriceCalculatorService
{
    decimal CalculateFinalPrice(Product product, CustomerType customerType);
}
```

## Checklist de Decisión

Antes de crear una interfaz, pregúntate:

1. **¿La necesita el dominio directamente?** → `Domain/Interfaces/`
2. **¿Coordina múltiples operaciones o servicios externos?** → `Application/Interfaces/`
3. **¿Es específica de una tecnología?** → `Infrastructure/Interfaces/`
4. **¿Es un patrón establecido como MediatR?** → Usar el patrón tal como está

## Beneficios Obtenidos

- ✅ **Claridad**: Cada interfaz tiene una ubicación lógica
- ✅ **Testabilidad**: Fácil creación de mocks por capa
- ✅ **Mantenibilidad**: Cambios en una capa no afectan otras
- ✅ **Flexibilidad**: Fácil intercambio de implementaciones
- ✅ **Cumple SOLID**: Especialmente DIP e ISP

## Recursos Creados

1. **[CLEAN_ARCHITECTURE_INTERFACES.md](CLEAN_ARCHITECTURE_INTERFACES.md)** - Guía teórica completa
2. **[INTERFACE_PLACEMENT_COMPARISON.md](INTERFACE_PLACEMENT_COMPARISON.md)** - Comparación práctica
3. **Ejemplos de código** en:
   - `src/Core/Application/Interfaces/` - Servicios de aplicación
   - `src/Core/Application/Services/` - Implementación de ejemplo
   - `src/Infrastructure/Interfaces/` - Servicios técnicos
   - `src/Core/Domain/Interfaces/IDomainServices.cs` - Servicios de dominio

## Conclusión

La ubicación de interfaces en Clean Architecture no es arbitraria, sino que sigue principios claros:

- **Domain**: Define lo que necesita (contratos)
- **Application**: Coordina cómo se usan (orquestación) 
- **Infrastructure**: Implementa los detalles (tecnología)

Este proyecto ahora ejemplifica correctamente estos principios con documentación y ejemplos prácticos.