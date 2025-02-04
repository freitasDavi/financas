namespace Fintech.DTOs.Requests.Parceladas;

public class NovaDespesaParceladaRequest
{
    public DateTime Data { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal ValorParcela { get; set; }
    public decimal TotalParcela { get; set; }
    public string Descricao  { get; set; } = string.Empty;
}