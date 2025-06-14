using Microsoft.EntityFrameworkCore;
using RedisCachingWebApi.Application.Models.LoginJWT;

namespace RedisCachingWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}