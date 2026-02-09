using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiEcommerce.Models;
using ApiEcommerce.Models.DTOs;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiEcommerce.Repository;

public class UserRepository : IUserRepository
{

    public readonly ApplicationDbContext _db;

    private string? secretKey;
    public UserRepository(ApplicationDbContext db, IConfiguration configuration)
    {
        _db=db;
        secretKey= configuration.GetValue<string>("ApiSettings:SecretKey");
    }
   public User? GetUser(int id)
    {
     return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public ICollection<User> GetUsers()
    {
        return _db.Users.OrderBy(u=> u.Username).ToList();
    }

    public async Task<bool> IsUniqueUser(string username)
    {
        return ! await _db.Users.AnyAsync(u => u.Username.ToLower().Trim() == username.ToLower().Trim());
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

        var user = _db.Users.FirstOrDefault(u => u.Username.ToLower().Trim() == userLogindto.Username.ToLower().Trim());
        if (user == null)
        {
            return new UserLoginResponseDto()
            {
                
                Token= "",
                User= null,
                Message= "Username no encontrado"
            };
        }
        if(!BCrypt.Net.BCrypt.Verify(userLogindto.Password, user.Password))
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
        var key= Encoding.UTF8.GetBytes(secretKey);

        var tokenDescriptor= new SecurityTokenDescriptor
        {
            Subject= new ClaimsIdentity(new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
            }),
            Expires= DateTime.UtcNow.AddHours(2),
            SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token= handlerToken.CreateToken(tokenDescriptor);

        return new UserLoginResponseDto()
        {
            Token= handlerToken.WriteToken(token),
            User= new UserRegisterDto()
            {
                Username= user.Username,
                Name= user.Name,    
                Role= user.Role,
                Password= user.Password ?? ""
            } , 
            Message= "Usuario logueado correctamente"
        };

    }

    async Task<User> IUserRepository.Register(CreateUserDto createuserdto)
    {
        var encrtyptedPassword = BCrypt.Net.BCrypt.HashPassword(createuserdto.Password);
        var user = new User
        {
            Username= createuserdto.Username ?? "No username",
            Name= createuserdto.Name,
            Role= createuserdto.Role,
            Password= encrtyptedPassword
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    
    }
}
