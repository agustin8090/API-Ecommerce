using System;
using ApiEcommerce.Domain.Models;
using  ApiEcommerce.Application.DTOs;


namespace ApiEcommerce.Application.Interfaces;

public interface IUserRepository
{
ICollection<AplicationUser> GetUsers();
AplicationUser? GetUser(string userId);

  Task<bool> IsUniqueUser(string username);
 Task<UserLoginResponseDto> Login(UserLoginDto userLogindto);
 Task<UserDataDto> Register(CreateUserDto createuserdto);

}
