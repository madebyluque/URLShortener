using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using URLShortener.Domain.Entities.Links;
using URLShortener.Infrastructure.Data.Links.Configurations;

namespace URLShortener.Infrastructure.Data.Contexts;

public class UriShortenerContext(IConfiguration configuration) : DbContext
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public DbSet<Link> Links { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

        options.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LinkEntityConfiguration).Assembly);
    }
}
