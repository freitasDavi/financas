namespace Fintech.Entities;

public class DespesaProgramada : BaseEntity
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    
    public long CodigoUsuario { get; set; }
    public virtual User Usuario { get; set; }
    public virtual ICollection<Despesas> Despesas { get; set; }
}