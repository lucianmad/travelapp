using TravelApp.BusinessLogic.DTOs.Country;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface ICountryService
{
    Task<IEnumerable<CountryGetDto>> GetAllCountriesAsync();
    Task<CountryGetDto?> GetCountryByIdAsync(int countryId);
    Task<CountryGetDto> CreateCountryAsync(CountryCreateDto countryCreateDto);
    Task<CountryGetDto> UpdateCountryAsync(int countryId, CountryUpdateDto countryUpdateDto);
    Task DeleteCountryAsync(int countryId);
}