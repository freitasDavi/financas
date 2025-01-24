using Fintech.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Utils.Mappings;

public class DespesaMap : IEntityTypeConfiguration<Despesas>
{
    public void Configure(EntityTypeBuilder<Despesas> builder)
    {
        builder.ToTable("despesas");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Valor)
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(d => d.DespesaProgramada)
            .WithMany(p => p.Despesas)
            .HasForeignKey(d => d.CodigoDespesaProgramada);

        builder.HasOne(d => d.DespesaParcelada)
            .WithMany(p => p.Despesas)
            .HasForeignKey(d => d.CodigoDespesaParcelada);
        
        builder.HasOne(d => d.Usuario)
            .WithMany(u => u.Despesas)
            .HasForeignKey(d => d.CodigoUsuario);
    }
}