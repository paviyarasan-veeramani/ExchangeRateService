using ExchangeRateService.Core.Interfaces;
using ExchangeRateService.Core.Models;

namespace ExchangeRateService.Core.Services;

public class ConversionService : IConversionService
{
    private readonly IExchangeRateRepository _repo;

    public ConversionService(IExchangeRateRepository repo) => _repo = repo;

    public async Task<ConvertResult?> ConvertAsync(string from, string to, decimal amount)
    {
        from = from.Trim().ToUpperInvariant();
        to = to.Trim().ToUpperInvariant();

        if (from == to)
            return new ConvertResult(from, to, amount, 1m, amount);

        var direct = await _repo.GetPairAsync(from, to);
        if (direct is not null)
            return Build(from, to, amount, direct.Rate);

        var inverse = await _repo.GetPairAsync(to, from);
        if (inverse is not null && inverse.Rate != 0)
            return Build(from, to, amount, 1m / inverse.Rate);

        return null;
    }

    private static ConvertResult Build(string from, string to, decimal amount, decimal rate) =>
        new(from, to, amount, rate, Math.Round(amount * rate, 2));
}
