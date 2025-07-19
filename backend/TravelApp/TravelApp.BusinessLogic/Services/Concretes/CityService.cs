using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Mappers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class CityService : ICityService
{
    private ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    public async Task<IEnumerable<CityGetDto>> GetAllCitiesAsync()
    {
        var cities = await _cityRepository.GetAllAsync();
        return CityMapper.MapToCityGetDto(cities);
    }

    public async Task<IEnumerable<CityGetDto>> GetAllCitiesByCountryIdAsync(int countryId)
    {
        var cities = await _cityRepository.GetAllByCountryIdAsync(countryId);
        return CityMapper.MapToCityGetDto(cities);
    }

    public async Task<CityGetDto> GetCityByIdAsync(int cityId)
    {
        var city = await _cityRepository.GetByIdAsync(cityId);
        if (city == null)
        {
            throw new EntityNotFoundException("City", cityId);
        }
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task<CityGetDto> CreateCityAsync(CityCreateDto cityCreateDto)
    {
        var city = CityMapper.MapToCity(cityCreateDto);
        await _cityRepository.CreateAsync(city);
        
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task<CityGetDto> UpdateCityAsync(int cityId, CityUpdateDto cityUpdateDto)
    {
        var city = await _cityRepository.GetByIdAsync(cityId);
        if (city == null)
        {
            throw new EntityNotFoundException("City", cityId);
        }
        city.MapToCity(cityUpdateDto);
        await _cityRepository.UpdateAsync(cityId, city);
        
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task DeleteCityAsync(int cityId)
    {
        var city = await  _cityRepository.GetByIdAsync(cityId);
        if (city == null)
        {
            throw new EntityNotFoundException("City", cityId);
        }
        
        await _cityRepository.DeleteAsync(cityId);
    }
}