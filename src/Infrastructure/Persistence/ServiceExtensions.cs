using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            // ID Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof (GenericRepository<>));
            services.AddScoped<ISalesRepository, SalesRepository>();
            
            return services;
        }
    }
}
