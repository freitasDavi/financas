using Fintech.Entities;
using Fintech.Utils.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Utils;

public class DataContext : DbContext
{
    public DbSet<Despesas> Despesas { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DespesaProgramada> DespesaProgramada { get; set; }
    public DbSet<DespesaParcelada> DespesaParcelada { get; set; }

    public DataContext(DbContextOptions<DataContext> options) :  base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new DespesaMap());
        modelBuilder.ApplyConfiguration(new DespesaParceladaMap());
        modelBuilder.ApplyConfiguration(new DespesaProgramadaMap());
    }
}