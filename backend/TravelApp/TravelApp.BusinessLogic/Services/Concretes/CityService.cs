using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Mappers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

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

    public async Task<CityGetDto> GetCityByIdAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null)
        {
            throw new EntityNotFoundException("City", id);
        }
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task<CityGetDto> CreateCityAsync(CityCreateDto cityCreateDto)
    {
        var city = CityMapper.MapToCity(cityCreateDto);
        await _cityRepository.CreateAsync(city);
        
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task<CityGetDto> UpdateCityAsync(int id, CityUpdateDto cityUpdateDto)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null)
        {
            throw new EntityNotFoundException("City", id);
        }
        city.MapToCity(cityUpdateDto);
        await _cityRepository.UpdateAsync(id, city);
        
        return CityMapper.MapToCityGetDto(city);
    }

    public async Task DeleteCityAsync(int id)
    {
        var city = await  _cityRepository.GetByIdAsync(id);
        if (city == null)
        {
            throw new EntityNotFoundException("City", id);
        }
        
        await _cityRepository.DeleteAsync(id);
    }
}