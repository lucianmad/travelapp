using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface IAttractionService
{
    Task<IEnumerable<AttractionGetDto>> GetAllAttractionsAsync();
    Task<AttractionGetDto> GetAttractionByIdAsync(int id);
    Task<AttractionGetDto> CreateAttractionAsync(AttractionCreateDto attractionCreateDto);
    Task<AttractionGetDto> UpdateAttractionAsync(int id, AttractionUpdateDto attractionUpdateDto);
    Task DeleteAttractionAsync(int id);
}