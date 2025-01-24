namespace Fintech.Exceptions;

public class NotFoundException(string entityName) : Exception
{
    public new string Message { get; set; } = $"{entityName} was not found.";
}