using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiEcommerce.Domain.Models;
using ApiEcommerce.Infrastructure.Data;

using ApiEcommerce.Application.Interfaces;
using ApiEcommerce.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mapster;
using Microsoft.Extensions.Configuration;
namespace ApiEcommerce.Infrastructure.Repository;

public class UserRepository : IUserRepository
{

    public readonly ApplicationDbContext _db;

    private string? secretKey;

    private readonly UserManager<AplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        _userManager = userManager;
        _roleManager = roleManager;
    }
   public AplicationUser? GetUser(string  id)
    {
     return _db.AplicationUsers.FirstOrDefault(u => u.Id == id);
    }

    public ICollection<AplicationUser> GetUsers()
    {
        return _db.AplicationUsers.OrderBy(u=> u.UserName).ToList();
    }

    public async Task<bool> IsUniqueUser(string username)
    {

        if(string.IsNullOrEmpty(username))
        {
            throw new ArgumentNullException("Username es requerido");
        }
        return ! await _db.AplicationUsers.AnyAsync(u => u.UserName!.ToLower().Trim() == username.ToLower().Trim());
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLogindto)
    {
        
        if (string.IsNullOrEmpty(userLogindto.Username)){
            return new UserLoginResponseDto()
            {
                
                Token= "",
                User= null,
                Message= "Username es requerido"
            };
        }

       var user = await _userManager.FindByNameAsync(userLogindto.Username);
        if (user == null)
        {
            return new UserLoginResponseDto()
            {
                
                Token= "",
                User= null,
                Message= "Username no encontrado"
            };
        }

        if(userLogindto.Password == null)
        {
            return new UserLoginResponseDto()
            {
                
                Token= "",
                User= null,
                Message= "Password es requerido"
            };
        }

        bool isvalid= await _userManager.CheckPasswordAsync(user, userLogindto.Password);


        if(!isvalid)
        {
            return new UserLoginResponseDto()
            {
                
                Token= "",
                User= null,
                Message= "Credenciales incorrectas"
            };
        }

        
        //generamos token JWT
        var handlerToken= new JwtSecurityTokenHandler();
        if(string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException("Secret key no configurada");
        }
        var roles= await _userManager.GetRolesAsync(user);
        var key= Encoding.UTF8.GetBytes(secretKey);

        var tokenDescriptor= new SecurityTokenDescriptor
        {
            Subject= new ClaimsIdentity(new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
            }),
            Expires= DateTime.UtcNow.AddHours(2),
            SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token= handlerToken.CreateToken(tokenDescriptor);

        return new UserLoginResponseDto()
        {
            Token= handlerToken.WriteToken(token),
            User= user.Adapt<UserDataDto>(),
            Message= "Usuario logueado correctamente"
        };

    }

    async Task<UserDataDto> IUserRepository.Register(CreateUserDto createuserdto)
    {
         if (string.IsNullOrEmpty(createuserdto.Username)){
            throw new ArgumentNullException("Username es requerido");
        }

        

        if(createuserdto.Password == null)
        {
            throw new ArgumentNullException("Password es requerido");
        }

        var user= new AplicationUser()
        {
            UserName= createuserdto.Username,
            Email= createuserdto.Username,
            NormalizedEmail= createuserdto.Username.ToUpper(),
            Name= createuserdto.Name

        };  
        
        var result= await _userManager.CreateAsync(user, createuserdto.Password);
        
        if(result.Succeeded)
        {
            var userRole= createuserdto.Role ?? "User";
            var roleExists= await _roleManager.RoleExistsAsync(userRole);
            if(!roleExists)
            {
                var identityRole= new IdentityRole(userRole);
                await _roleManager.CreateAsync(identityRole);
            }
            await _userManager.AddToRoleAsync(user, userRole);

            var createdUser= _db.AplicationUsers.FirstOrDefault(u => u.UserName == createuserdto.Username);
            return createdUser.Adapt<UserDataDto>();
        }
        var errors= string.Join(", ", result.Errors.Select(e => e.Description));
        throw new ApplicationException($"No se puedo realizar el registro del usuario. Errores: {errors}");
    
    }
}
