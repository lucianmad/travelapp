using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Mappers;

public static class CityMapper
{
    public static IEnumerable<CityGetDto> MapToCityGetDto(IEnumerable<City> cities)
    {
        return cities.Select(MapToCityGetDto);
    }

    public static CityGetDto MapToCityGetDto(City city)
    {
        return new CityGetDto
        {
            Id = city.Id,
            Name = city.Name,
            Description = city.Description,
            AttractionsNames = city.Attractions.Select(a => a.Name),
            CountryName = city.Country.Name
        };
    }

    public static City MapToCity(CityCreateDto cityCreateDto)
    {
        return new City
        {
            CountryId = cityCreateDto.CountryId,
            Name = cityCreateDto.Name,
            Description = cityCreateDto.Description,
        };
    }

    public static void MapToCity(this City city, CityUpdateDto cityUpdateDto)
    {
        city.CountryId = cityUpdateDto.CountryId;
        city.Name = cityUpdateDto.Name;
        city.Description = cityUpdateDto.Description;
    }
}