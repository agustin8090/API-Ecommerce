// Mapster configuration for Category
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dto;
using Mapster;

namespace ApiEcommerce.Mapping;

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
