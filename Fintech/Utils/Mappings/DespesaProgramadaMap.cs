using Fintech.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Utils.Mappings;

public class DespesaProgramadaMap : IEntityTypeConfiguration<DespesaProgramada>
{
    public void Configure(EntityTypeBuilder<DespesaProgramada> builder)
    {
        builder.ToTable("despesas_programadas");
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Valor)
            .HasColumnType("decimal(18, 2)");
        
        builder.HasOne(d => d.Usuario)
            .WithMany(u => u.DespesaProgramada)
            .HasForeignKey(d => d.CodigoUsuario);
    }
}