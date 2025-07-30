using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Mappers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class AttractionService : IAttractionService
{
    private readonly IAttractionRepository _attractionRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;

    public AttractionService(IAttractionRepository attractionRepository, ICityRepository cityRepository, ICountryRepository countryRepository)
    {
        _attractionRepository = attractionRepository;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
    }
    
    public async Task<IEnumerable<AttractionGetDto>> GetAllAttractionsAsync(int? cityId = null, int? countryId = null, string? cityName = null, string? countryName = null)
    {
        if (cityId.HasValue)
        {
            if(!await _cityRepository.ExistsByIdAsync(cityId.Value))
            {
                throw new InvalidFilterException("city Id", cityId.Value.ToString());
            }
        }

        if (!string.IsNullOrEmpty(cityName))
        {
            if(!await _cityRepository.ExistsByNameAsync(cityName))
            {
                throw new InvalidFilterException("city name", cityName);
            }       
        }
        
        if (countryId.HasValue)
        {
            if (!await _countryRepository.ExistsByIdAsync(countryId.Value))
            {
                throw new InvalidFilterException("country Id", countryId.Value.ToString());
            }
        }

        if (!string.IsNullOrEmpty(countryName))
        {
            if(!await _countryRepository.ExistsByNameAsync(countryName))
            {
                throw new InvalidFilterException("country name", countryName);
            }      
        }
        
        var attractions = await _attractionRepository.GetAllAsync(cityId, countryId, cityName, countryName);
        return AttractionMapper.MapToAttractionGetDto(attractions);
    }

    public async Task<AttractionGetDto> GetAttractionByIdAsync(int id)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task<AttractionGetDto> CreateAttractionAsync(AttractionCreateDto attractionCreateDto)
    {
        await ValidateCityExists(attractionCreateDto.CityId);
        await ValidateUniqueAttractionNameInCity(attractionCreateDto.Name, attractionCreateDto.CityId);
        var attraction = AttractionMapper.MapToAttraction(attractionCreateDto);
        await _attractionRepository.CreateAsync(attraction);
        
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task<AttractionGetDto> UpdateAttractionAsync(int id, AttractionUpdateDto attractionUpdateDto)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        
        await ValidateCityExists(attractionUpdateDto.CityId);
        await ValidateUniqueAttractionNameInCityExcludingId(attractionUpdateDto.Name, id, attractionUpdateDto.CityId);
        
        attraction.MapToAttraction(attractionUpdateDto);
        await _attractionRepository.UpdateAsync(id, attraction);
        
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task DeleteAttractionAsync(int id)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        
        await _attractionRepository.DeleteAsync(id);
    }

    private async Task ValidateUniqueAttractionNameInCity(string name, int cityId)
    {
        if (await _attractionRepository.ExistsByNameAndCityAsync(name, cityId))
        {
            throw new DuplicateEntityException("Attraction", name);
        }
    }
    
    private async Task ValidateUniqueAttractionNameInCityExcludingId(string name, int id, int cityId)
    {
        if (await _attractionRepository.ExistsByNameAndCityExcludingIdAsync(name, id, cityId))
        {
            throw new DuplicateEntityException("Attraction", name);
        }
    }
    
    private async Task ValidateCityExists(int cityId)
    {
        if (!await _cityRepository.ExistsByIdAsync(cityId))
        {
            throw new EntityNotFoundException("City", cityId);
        }
    }
}