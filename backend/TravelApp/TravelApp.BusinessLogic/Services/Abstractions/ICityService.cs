using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface ICityService
{
    Task<IEnumerable<CityGetDto>> GetAllCitiesAsync();
    Task<IEnumerable<CityGetDto>> GetAllCitiesByCountryIdAsync(int countryId);
    Task<CityGetDto> GetCityByIdAsync(int id);
    Task<CityGetDto> CreateCityAsync(CityCreateDto cityCreateDto);
    Task<CityGetDto> UpdateCityAsync(int id, CityUpdateDto cityUpdateDto);
    Task DeleteCityAsync(int id);
}