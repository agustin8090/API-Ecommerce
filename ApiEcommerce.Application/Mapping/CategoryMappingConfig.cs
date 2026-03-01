// Mapster configuration for Category
using ApiEcommerce.Application.DTOs;
using ApiEcommerce.Domain.Models;
using Mapster;

namespace ApiEcommerce.Application.Mapping;

public static class CategoryMappingConfig
{
    public static void RegisterCategoryMappings()
    {
        TypeAdapterConfig<Category, CategoryDto>.NewConfig();
        TypeAdapterConfig<Category, CreateCategoryDto>.NewConfig();
        TypeAdapterConfig<CategoryDto, Category>.NewConfig();
        TypeAdapterConfig<CreateCategoryDto, Category>.NewConfig();
    }
}
