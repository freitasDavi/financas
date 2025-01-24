namespace Fintech.DTOs.Responses;

public class BadRequestResponse
{
    public string Message { get; set; }
    public string Details { get; set; }
    public object Data { get; set; }
}