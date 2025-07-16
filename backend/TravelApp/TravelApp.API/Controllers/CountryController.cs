using Microsoft.AspNetCore.Mvc;
using TravelApp.BusinessLogic.DTOs.Country;
using TravelApp.BusinessLogic.Services.Abstractions;

namespace TravelApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryGetDto>>> GetAllAsync()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries);
    }
}