namespace Fintech.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public bool Premium { get; set; }
    public bool Active { get; set; }
}