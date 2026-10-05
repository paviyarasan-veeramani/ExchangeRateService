using ExchangeRateService.Core.Models;

namespace ExchangeRateService.Core.Interfaces;

public interface IConversionService
{
    /// <summary>Returns null when no rate exists for the pair.</summary>
    Task<ConvertResult?> ConvertAsync(string from, string to, decimal amount);
}
