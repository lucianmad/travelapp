using TravelApp.BusinessLogic.DTOs.Country;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Mappers;

public static class CountryMapper
{
    public static IEnumerable<CountryGetDto> MapToCountryGetDto(IEnumerable<Country> countries)
    {
        return countries.Select(MapToCountryGetDto);
    }
    
    public static CountryGetDto MapToCountryGetDto(Country country)
    {
        return new CountryGetDto
        {
            Id = country.Id,
            Name = country.Name,
            Flag = country.Flag,
            CityNames = country.Cities.Select(c => c.Name)
        };
    }

    public static Country MapToCountry(CountryCreateDto countryCreateDto)
    {
        return new Country
        {
            Name = countryCreateDto.Name,
            Flag = countryCreateDto.Flag
        };
    }

    public static void MapToCountry(this Country country, CountryUpdateDto countryUpdateDto)
    {
        country.Name = countryUpdateDto.Name;
        country.Flag = countryUpdateDto.Flag;
    }
}