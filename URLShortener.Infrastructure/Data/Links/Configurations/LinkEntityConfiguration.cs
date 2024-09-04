using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Domain.Entities.Links;

namespace URLShortener.Infrastructure.Data.Links.Configurations;
internal class LinkEntityConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(10)
            .IsFixedLength();

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion(v => v.ToString(), v => new Uri(v));

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}
