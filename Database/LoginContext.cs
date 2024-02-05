using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Database;

public class LoginContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            string connectionString = Environment.GetEnvironmentVariable("SIS_LOGIN_CONNECTION_STRING") ?? throw new InvalidOperationException(
            "A string de conexão com o banco de dados não foi encontrada");

            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        catch (InvalidOperationException dbException) when (dbException.InnerException is MySqlException mySqlException)
        {
            throw mySqlException;
        }
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

        // Configurações para mapear as subtipos de usuários para a tabela Users
        modelBuilder.Entity<Administrator>().ToTable("Users");
        modelBuilder.Entity<Client>().ToTable("Users");
        modelBuilder.Entity<Visitor>().ToTable("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LoginContext).Assembly);
    }

}