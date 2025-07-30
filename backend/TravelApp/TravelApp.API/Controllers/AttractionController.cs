using Microsoft.AspNetCore.Mvc;
using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.BusinessLogic.Services.Abstractions;

namespace TravelApp.API.Controllers;

[Route("api/attractions")]
[ApiController]
public class AttractionController : ControllerBase
{
    private readonly IAttractionService  _attractionService;

    public AttractionController(IAttractionService attractionService)
    {
        _attractionService = attractionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? cityId = null, [FromQuery] int? countryId = null, [FromQuery] string? cityName = null, [FromQuery] string? countryName = null)
    {
        var attractions = await _attractionService.GetAllAttractionsAsync(cityId, countryId, cityName, countryName);
        return Ok(attractions);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var attraction = await _attractionService.GetAttractionByIdAsync(id);
        return Ok(attraction);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttraction([FromBody] AttractionCreateDto attractionCreateDto)
    {
        var attraction = await _attractionService.CreateAttractionAsync(attractionCreateDto);
        return CreatedAtAction(nameof(GetById), new {id = attraction.Id}, attraction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttraction([FromRoute] int id, [FromBody] AttractionUpdateDto attractionUpdateDto)
    {
        var attraction = await _attractionService.UpdateAttractionAsync(id, attractionUpdateDto);
        return Ok(attraction);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttraction([FromRoute] int id)
    {
        await _attractionService.DeleteAttractionAsync(id);
        return NoContent();
    }
}