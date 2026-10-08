using Microsoft.EntityFrameworkCore;
using SafeVault.Models;
namespace SafeVault.Data;

public class SafeVaultDbContext : DbContext
{
    public SafeVaultDbContext(DbContextOptions<SafeVaultDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}