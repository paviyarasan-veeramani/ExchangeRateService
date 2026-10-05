using ExchangeRateService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ExchangeRateEntity = ExchangeRateService.Core.Entities.ExchangeRate;


namespace ExchangeRateService.Infrastructure.Data;

public class ExchangeRateDbContext : DbContext
{
    public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options) : base(options) { }

    public DbSet<ExchangeRateEntity> ExchangeRates => Set<ExchangeRateEntity>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<ExchangeRateEntity>(e =>
        {
            e.Property(x => x.FromCurrency).HasMaxLength(3).IsRequired();
            e.Property(x => x.ToCurrency).HasMaxLength(3).IsRequired();
            e.Property(x => x.Rate).HasPrecision(18, 6);
            e.HasIndex(x => new { x.FromCurrency, x.ToCurrency }).IsUnique();

            var stamp = new DateTime(2026, 10, 1, 10, 30, 0, DateTimeKind.Utc);
            e.HasData(
                new ExchangeRateEntity { Id = 1, FromCurrency = "USD", ToCurrency = "INR", Rate = 83.2150m,  UpdatedAt = stamp },
                new ExchangeRateEntity { Id = 2, FromCurrency = "EUR", ToCurrency = "INR", Rate = 91.4800m,  UpdatedAt = stamp },
                new ExchangeRateEntity { Id = 3, FromCurrency = "GBP", ToCurrency = "INR", Rate = 108.7300m, UpdatedAt = stamp });
        });
    }
}
