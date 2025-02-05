namespace Fintech.DTOs.Responses;

public class GraficosBaseResponse
{
    public ICollection<string> Descriptions { get; set; } = new List<string>();
    public ICollection<decimal> Values { get; set; } = new List<decimal>();
}