using System.Data;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class DataContext : DbContext
{
    
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region ToDoModelConfig

        modelBuilder.Entity<ToDo>()
            .Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever();

        modelBuilder.Entity<ToDo>()
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<ToDo>()
            .Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(255);
        
        modelBuilder.Entity<ToDo>()
            .Property(b => b.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<ToDo>()
            .Property(b => b.UserId)
            .IsRequired();

        #endregion

        #region UserConfig

        //Id
        modelBuilder.Entity<User>()
            .Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever();

        //Username
        modelBuilder.Entity<User>()
            .Property(b => b.Username)
            .IsRequired()
            .HasMaxLength(22);

        modelBuilder.Entity<User>()
            .HasIndex(b => b.Username)
            .IsUnique();

        //Password
        modelBuilder.Entity<User>()
            .Property(b => b.Password)
            .IsRequired();

        //Email
        modelBuilder.Entity<User>()
            .Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(320);

        modelBuilder.Entity<User>()
            .HasIndex(b => b.Email)
            .IsUnique();

        #endregion

        #region RefreshTokenConfig

        modelBuilder.Entity<RefreshToken>()
            .Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever();

        modelBuilder.Entity<RefreshToken>()
            .Property(b => b.Token)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(b => b.CreatedAt)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(b => b.Expires)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(b => b.UserId)
            .IsRequired();

        #endregion

    }
}