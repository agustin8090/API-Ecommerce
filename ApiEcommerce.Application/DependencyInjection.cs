using System;
using Microsoft.Extensions.DependencyInjection;
using ApiEcommerce.Application.Mapping;
namespace ApiEcommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ProductMappingConfig.RegisterProductMappings();
        CategoryMappingConfig.RegisterCategoryMappings();
        UserMappingConfig.RegisterUserMappings();
        
        return services;
    }
}