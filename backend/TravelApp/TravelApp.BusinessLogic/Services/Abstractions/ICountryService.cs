using TravelApp.BusinessLogic.DTOs.Country;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface ICountryService
{ 
    Task<IEnumerable<CountryGetDto>> GetAllCountriesAsync();
    Task<CountryGetDto> GetCountryByIdAsync(int id);
    Task<CountryGetDto> CreateCountryAsync(CountryCreateDto countryCreateDto);
}