using ExchangeRateService.Core.Entities;
using ExchangeRateService.Core.Interfaces;
using ExchangeRateService.Core.Services;
using Xunit;

namespace ExchangeRateService.Tests;

public class ConversionServiceTests
{
    // In-memory fake: no database needed, so tests run fast on Jenkins
    private class FakeRepo : IExchangeRateRepository
    {
        private readonly List<ExchangeRate> _rates = new()
        {
            new() { Id = 1, FromCurrency = "USD", ToCurrency = "INR", Rate = 83.2150m, UpdatedAt = DateTime.UtcNow }
        };
        public Task<IReadOnlyList<ExchangeRate>> GetAllAsync() => Task.FromResult<IReadOnlyList<ExchangeRate>>(_rates);
        public Task<ExchangeRate?> GetPairAsync(string from, string to) =>
            Task.FromResult(_rates.FirstOrDefault(r => r.FromCurrency == from && r.ToCurrency == to));
    }

    [Fact]
    public async Task Convert_UsdToInr_ReturnsCorrectAmount()
    {
        var service = new ConversionService(new FakeRepo());

        var result = await service.ConvertAsync("USD", "INR", 100m);

        Assert.NotNull(result);
        Assert.Equal(8321.50m, result!.Result);
    }

    [Fact]
    public async Task Convert_SameCurrency_ReturnsSameAmount()
    {
        var service = new ConversionService(new FakeRepo());

        var result = await service.ConvertAsync("usd", "USD", 250m);

        Assert.NotNull(result);
        Assert.Equal(250m, result!.Result);
        Assert.Equal(1m, result.Rate);
    }
}
