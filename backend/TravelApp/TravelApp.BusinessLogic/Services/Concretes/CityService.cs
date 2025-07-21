using TravelApp.BusinessLogic.Dtos.City;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Mappers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;

    public CityService(ICityRepository cityRepository, ICountryRepository countryRepository)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
    }
    
    public async Task<IEnumerable<CityGetDto>> GetAllCitiesAsync(string? countryName = null, int? countryId = null)
    {
        if (countryId.HasValue)
        {
            if(!await _countryRepository.ExistsByIdAsync(countryId.Value))
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
        
        var cities = await _cityRepository.GetAllAsync(countryName, countryId);
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
        await ValidateCountryExists(cityCreateDto.CountryId);
        await ValidateUniqueCityNameInCountry(cityCreateDto.Name, cityCreateDto.CountryId);
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
        
        await ValidateCountryExists(cityUpdateDto.CountryId);
        await ValidateUniqueCityNameInCountryExcludingId(cityUpdateDto.Name, id, cityUpdateDto.CountryId);
        
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

    private async Task ValidateUniqueCityNameInCountry(string name, int countryId)
    {
        if (await _cityRepository.ExistsByNameAndCountryAsync(name, countryId))
        {
            throw new DuplicateEntityException("City", name);
        }
    }
    
    private async Task ValidateUniqueCityNameInCountryExcludingId(string name, int id, int countryId)
    {
        if (await _cityRepository.ExistsByNameAndCountryExcludingIdAsync(name, id, countryId))
        {
            throw new DuplicateEntityException("City", name);
        }
    }
    
    private async Task ValidateCountryExists(int countryId)
    {
        if (!await _countryRepository.ExistsByIdAsync(countryId))
        {
            throw new EntityNotFoundException("Country", countryId);
        }
    }
}