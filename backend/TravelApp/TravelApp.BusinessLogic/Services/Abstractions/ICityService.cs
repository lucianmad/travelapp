using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface ICityService
{
    Task<IEnumerable<CityGetDto>> GetAllCitiesAsync(string? countryName = null, int? countryId = null);
    Task<CityGetDto> GetCityByIdAsync(int id);
    Task<CityGetDto> CreateCityAsync(CityCreateDto cityCreateDto);
    Task<CityGetDto> UpdateCityAsync(int id, CityUpdateDto cityUpdateDto);
    Task DeleteCityAsync(int id);
}