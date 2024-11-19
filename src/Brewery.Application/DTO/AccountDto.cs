namespace Brewery.Application.DTO;

public class AccountDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
}