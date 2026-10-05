namespace ExchangeRateService.Core.Models;

public record ConvertResult(string From, string To, decimal Amount, decimal Rate, decimal Result);
