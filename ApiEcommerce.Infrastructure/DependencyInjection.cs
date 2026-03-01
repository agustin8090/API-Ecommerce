using ApiEcommerce.Application.Interfaces;
using ApiEcommerce.Domain.IRepository;
using ApiEcommerce.Domain.Models;
using ApiEcommerce.Infrastructure.Data;
using ApiEcommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiEcommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. DbContext
        var connectionString = configuration.GetConnectionString("ConexionSql");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .UseSeeding((context, _) => {
                DataSeeder.SeedData((ApplicationDbContext)context);
            })
        );

        // 2. Identity
        services.AddIdentity<AplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
        // 3. Repositorios
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}