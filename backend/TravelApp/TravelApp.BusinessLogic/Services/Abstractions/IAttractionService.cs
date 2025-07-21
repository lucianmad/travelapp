using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface IAttractionService
{
    Task<IEnumerable<AttractionGetDto>> GetAllAttractionsAsync(int? cityId = null, int? countryId = null, string? cityName = null, string? countryName = null);
    Task<AttractionGetDto> GetAttractionByIdAsync(int id);
    Task<AttractionGetDto> CreateAttractionAsync(AttractionCreateDto attractionCreateDto);
    Task<AttractionGetDto> UpdateAttractionAsync(int id, AttractionUpdateDto attractionUpdateDto);
    Task DeleteAttractionAsync(int id);
}