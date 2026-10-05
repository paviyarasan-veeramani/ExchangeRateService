using ExchangeRateService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateService.Api.Controllers;

[ApiController]
[Route("api/convert")]
public class ConvertController : ControllerBase
{
    private readonly IConversionService _conversion;

    public ConvertController(IConversionService conversion) => _conversion = conversion;

    // GET /api/convert?from=USD&to=INR&amount=100
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string from, [FromQuery] string to, [FromQuery] decimal amount)
    {
        if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            return BadRequest(new { error = "Both 'from' and 'to' currencies are required." });
        if (amount <= 0)
            return BadRequest(new { error = "Amount must be greater than 0." });

        var result = await _conversion.ConvertAsync(from, to, amount);
        return result is null
            ? NotFound(new { error = $"No rate available for {from.ToUpperInvariant()} to {to.ToUpperInvariant()}." })
            : Ok(result);
    }
}
