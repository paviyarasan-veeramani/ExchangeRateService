using ExchangeRateService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateService.Api.Controllers;

[ApiController]
[Route("api/rates")]
public class RatesController : ControllerBase
{
    private readonly IExchangeRateRepository _repo;

    public RatesController(IExchangeRateRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
}
