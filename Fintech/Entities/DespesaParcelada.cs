namespace Fintech.Entities;

public class DespesaParcelada : BaseEntity
{
    public DateTime Data { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal ValorParcela { get; set; }
    public decimal TotalParcela { get; set; }
    public string Descricao  { get; set; } = string.Empty;
    
    public long CodigoUsuario { get; set; }
    public virtual User Usuario { get; set; }
    
    public virtual ICollection<Despesas> Despesas { get; set; }
}