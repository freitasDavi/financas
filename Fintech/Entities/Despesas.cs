using Fintech.Enums;

namespace Fintech.Entities;

public class Despesas : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Origem { get; set; } = string.Empty;
    public FormaDePagamento FormaDePagamento { get; set; }
    public decimal Valor { get; set; }
    
    public long CodigoUsuario { get; set; }
    public virtual User Usuario { get; set; }
    
    public long? CodigoDespesaProgramada { get; set; }
    public virtual DespesaProgramada? DespesaProgramada { get; set; }
    
    public long? CodigoDespesaParcelada { get; set; }
    public virtual DespesaParcelada? DespesaParcelada { get; set; }
}