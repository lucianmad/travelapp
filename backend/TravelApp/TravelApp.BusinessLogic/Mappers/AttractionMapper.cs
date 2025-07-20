using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Mappers;

public static class AttractionMapper
{
    public static IEnumerable<AttractionGetDto> MapToAttractionGetDto(IEnumerable<Attraction> attractions)
    {
        return attractions.Select(MapToAttractionGetDto);
    }

    public static AttractionGetDto MapToAttractionGetDto(Attraction attraction)
    {
        return new AttractionGetDto
        {
            Id = attraction.Id,
            Name = attraction.Name,
            Description = attraction.Description,
            CityName = attraction.City.Name,
            CountryName = attraction.City.Country.Name,
        };
    }

    public static Attraction MapToAttraction(AttractionCreateDto attractionCreateDto)
    {
        return new Attraction
        {
            CityId = attractionCreateDto.CityId,
            Name = attractionCreateDto.Name,
            Description = attractionCreateDto.Description,
        };
    }

    public static void MapToAttraction(this Attraction attraction, AttractionUpdateDto attractionUpdateDto)
    {
        attraction.CityId = attractionUpdateDto.CityId;
        attraction.Name = attractionUpdateDto.Name;
        attraction.Description = attractionUpdateDto.Description;
    }
}