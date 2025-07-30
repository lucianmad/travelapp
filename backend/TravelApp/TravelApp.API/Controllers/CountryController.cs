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
    [ProducesResponseType(typeof(IEnumerable<CountryGetDto>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CountryGetDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(typeof(CountryGetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDto countryCreateDto)
    {
        var country = await _countryService.CreateCountryAsync(countryCreateDto);
        return CreatedAtAction(nameof(GetById), new {id = country.Id}, country);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CountryGetDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCountry([FromRoute] int id, [FromBody] CountryUpdateDto countryUpdateDto)
    {
        var country = await _countryService.UpdateCountryAsync(id, countryUpdateDto);
        return Ok(country);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCountry([FromRoute] int id)
    {
        await _countryService.DeleteCountryAsync(id);
        return NoContent();
    }
}