namespace Brewery.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User(Guid id, string email, string password, string role,
        Dictionary<string, IEnumerable<string>> claims, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        Claims = claims;
        CreatedAt = createdAt;
    }
}