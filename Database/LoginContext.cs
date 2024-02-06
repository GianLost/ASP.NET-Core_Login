using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Database;

public class LoginContext : DbContext
{
    public LoginContext(DbContextOptions<LoginContext> options) : base(options)
    {

    }

    public DbSet<Users>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasIndex(u => u.Id).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.Name).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.Login).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.CellPhone).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.Password).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.SessionToken).IsUnique();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LoginContext).Assembly);
    }

}