namespace Fintech.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public bool Premium { get; set; }
    public bool Active { get; set; }
    
    public virtual ICollection<Despesas> Despesas { get; set; }
    public virtual ICollection<DespesaProgramada> DespesaProgramada { get; set; }
    public virtual ICollection<DespesaParcelada> DespesaParcelada { get; set; }
}