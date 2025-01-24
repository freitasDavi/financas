using Fintech.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Utils.Mappings;

public class DespesaParceladaMap : IEntityTypeConfiguration<DespesaParcelada>
{
    public void Configure(EntityTypeBuilder<DespesaParcelada> builder)
    {
        builder.ToTable("despesas_parceladas");
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.TotalParcela)
            .HasColumnType("decimal(18, 2)");
        
        builder.Property(e => e.ValorParcela)
            .HasColumnType("decimal(18, 2)");
        
        builder.HasOne(d => d.Usuario)
            .WithMany(u => u.DespesaParcelada)
            .HasForeignKey(d => d.CodigoUsuario);
    }
}