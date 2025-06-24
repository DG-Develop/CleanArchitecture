using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ECommerce.Persistence.Repositories;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain;
using ECommerce.Persistence.ECommerceDbContext;

namespace ECommerce.Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcommerceDbContext>(options =>
                options
                //.UseLazyLoadingProxies() // <----- HERE
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton(connectionString); // Inyecta la cadena de conexión como string


            //services.Configure<ApplicationSettingsDTO>(configuration.GetConnectionString("DefaultConnection"));
            //var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<CadenasConexionDbDTO>()!;

            services.AddScoped(typeof(IGenericRepository<>), typeof (GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
