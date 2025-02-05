namespace Fintech.DTOs.Requests.Programadas;

public class NovaDespesaProgramadaRequest
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
}