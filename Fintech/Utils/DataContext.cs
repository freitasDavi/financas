using Fintech.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Utils;

public class DataContext : DbContext
{
    public DbSet<Despesas> Despesas { get; set; }

    public DataContext(DbContextOptions<DataContext> options) :  base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Despesas>(entity =>
        {
            entity.ToTable("despesas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)");
        });
    }
}