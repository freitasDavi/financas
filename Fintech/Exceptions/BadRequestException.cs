namespace Fintech.Exceptions;

public class BadRequestException : Exception
{
    public string Message { get; }
    public string Details { get; }
    public object Data { get; }
    
    public BadRequestException(string message) 
    {
        this.Message = message;
    }
    
    public BadRequestException(string message, string details) 
    {
        this.Message = message;
        this.Details = details;
    }
    
    public BadRequestException(string message, string details, object data) 
    {
        this.Message = message;
        this.Details = details;
        this.Data = data;
    }
}