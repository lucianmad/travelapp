using Microsoft.AspNetCore.Mvc;
using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.BusinessLogic.Services.Abstractions;

namespace TravelApp.API.Controllers;

[Route("api/cities")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityService  _cityService;

    public CityController(ICityService cityService)
    {   
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? countryName = null, [FromQuery] int? countryId = null)
    {
        var cities = await _cityService.GetAllCitiesAsync(countryName, countryId);
        return Ok(cities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CityCreateDto cityCreateDto)
    {
        var city = await _cityService.CreateCityAsync(cityCreateDto);
        return CreatedAtAction(nameof(GetById), new {id = city.Id}, city);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCity([FromRoute] int id, [FromBody] CityUpdateDto cityUpdateDto)
    {
        var city  = await _cityService.UpdateCityAsync(id, cityUpdateDto);
        return Ok(city);
    }

    [HttpDelete("{id}")]
    public async Task DeleteCity([FromRoute] int id)
    {
        await _cityService.DeleteCityAsync(id);
    }
}