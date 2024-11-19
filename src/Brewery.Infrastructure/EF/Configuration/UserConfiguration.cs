using System.Text.Json;
using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brewery.Infrastructure.EF.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Claims)
            .HasConversion(c => JsonSerializer.Serialize(c, _jsonOptions),
                json => JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(json, _jsonOptions));
    }
}