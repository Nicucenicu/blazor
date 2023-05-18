using BlazorShop.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Server
{
     public class AppDbContext : DbContext
     {
          public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
          public DbSet<Product> Product { get; set; }
          public DbSet<UserDb> User { get; set; }
          
     }
}