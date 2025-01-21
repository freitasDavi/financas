using Fintech.Enums;

namespace Fintech.DTOs.Requests;

public class NovaDespesaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Origem { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public FormaDePagamento FormaDePagamento { get; set; }
    public decimal Valor { get; set; }
}