// Mapster configuration for User
using ApiEcommerce.Domain.Models;
using ApiEcommerce.Application.DTOs;
using Mapster;

namespace ApiEcommerce.Application.Mapping;

public static class UserMappingConfig
{
    public static void RegisterUserMappings()
    {
        TypeAdapterConfig<User, UserDto>.NewConfig();
        TypeAdapterConfig<CreateUserDto, User>.NewConfig();
        TypeAdapterConfig<UserLoginDto, User>.NewConfig();
        TypeAdapterConfig<UserLoginResponseDto, User>.NewConfig();
        TypeAdapterConfig<AplicationUser, UserDataDto>.NewConfig();
        TypeAdapterConfig<AplicationUser, UserDto>.NewConfig();
        // Add reverse maps if needed
    }
}
