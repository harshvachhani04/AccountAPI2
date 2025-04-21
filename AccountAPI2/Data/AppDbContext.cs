using AccountAPI2.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AccountAPI2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User user = new User()
            {
                UserId = 1,
                Username = "admin",
                Password = "admin",
                ConfirmPassword = "admin",
                Email = "admin@gmail.com",
                Role = "admin",
            };
            modelBuilder.Entity<User>().HasData(user);
        }
        public DbSet<User> Users { get; set; }
    }
}
