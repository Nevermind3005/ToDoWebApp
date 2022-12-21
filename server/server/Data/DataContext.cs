using System.Data;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }

    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    }
}