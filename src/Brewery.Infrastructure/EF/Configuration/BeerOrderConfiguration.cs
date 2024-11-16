using Brewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brewery.Infrastructure.EF.Configuration;

public class BeerOrderConfiguration : IEntityTypeConfiguration<BeerOrder>
{
    public void Configure(EntityTypeBuilder<BeerOrder> builder)
    {
        builder.HasKey(b => b.BeerId);
    }
}