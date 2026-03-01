using ApiEcommerce.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<AplicationUser>
{
    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories {get;set; }
    public DbSet<Product> Products {get;set; }
    public DbSet<AplicationUser> AplicationUsers {get;set; }
 

}