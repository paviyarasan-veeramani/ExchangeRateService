using ExchangeRateService.Core.Entities;
using ExchangeRateService.Core.Interfaces;
using ExchangeRateService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using ExchangeRateEntity = ExchangeRateService.Core.Entities.ExchangeRate;

namespace ExchangeRateService.Infrastructure.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ExchangeRateDbContext _db;

    public ExchangeRateRepository(ExchangeRateDbContext db) => _db = db;

    public async Task<IReadOnlyList<ExchangeRateEntity>> GetAllAsync() =>
        await _db.ExchangeRates.AsNoTracking().OrderBy(r => r.FromCurrency).ThenBy(r => r.ToCurrency).ToListAsync();

    public Task<ExchangeRateEntity?> GetPairAsync(string from, string to) =>
        _db.ExchangeRates.AsNoTracking().FirstOrDefaultAsync(r => r.FromCurrency == from && r.ToCurrency == to);

}
