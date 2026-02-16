// Mapster configuration for Product
using ApiEcommerce.Models;
using ApiEcommerce.Models.DTOs;
using Mapster;

namespace ApiEcommerce.Mapping;

public static class ProductMappingConfig
{
    public static void RegisterProductMappings()
    {
        TypeAdapterConfig<Product, ProductDto>
            .NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
        TypeAdapterConfig<Product, CreateProductDto>.NewConfig();
        TypeAdapterConfig<Product, UpdateProductDto>.NewConfig();
        TypeAdapterConfig<ProductDto, Product>.NewConfig();
        TypeAdapterConfig<CreateProductDto, Product>.NewConfig();
        TypeAdapterConfig<UpdateProductDto, Product>.NewConfig();
    }
}
