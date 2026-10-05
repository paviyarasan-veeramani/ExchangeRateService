using ExchangeRateService.Core.Entities;

namespace ExchangeRateService.Core.Interfaces;

public interface IExchangeRateRepository
{
    Task<IReadOnlyList<ExchangeRate>> GetAllAsync();
    Task<ExchangeRate?> GetPairAsync(string from, string to);
}
