using Microsoft.AspNetCore.Mvc;
using TravelApp.BusinessLogic.DTOs.Country;
using TravelApp.BusinessLogic.Services.Abstractions;

namespace TravelApp.API.Controllers;

[Route("api/countries")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var country = await _countryService.GetCountryByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDto countryCreateDto)
    {
        var country = await _countryService.CreateCountryAsync(countryCreateDto);
        return CreatedAtAction(nameof(GetById), new {id = country.Id}, country);
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateCountry([FromRoute] int id, [FromBody] CountryUpdateDto countryUpdateDto)
    {
        var country = await _countryService.UpdateCountryAsync(id, countryUpdateDto);
        return Ok(country);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteCountry(int id)
    {
        await _countryService.DeleteCountryAsync(id);
    }
}