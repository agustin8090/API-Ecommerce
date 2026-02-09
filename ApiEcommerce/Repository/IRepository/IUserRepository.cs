using System;
using ApiEcommerce.Models;
using ApiEcommerce.Models.DTOs;

namespace ApiEcommerce.Repository.IRepository;

public interface IUserRepository
{
ICollection<User> GetUsers();
User? GetUser(int userId);

  Task<bool> IsUniqueUser(string username);
 Task<UserLoginResponseDto> Login(UserLoginDto userLogindto);
 Task<User> Register(CreateUserDto createuserdto);

}
