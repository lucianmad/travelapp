using TravelApp.BusinessLogic.DTOs.Country;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;
using TravelApp.BusinessLogic.Mappers;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    
    public async Task<IEnumerable<CountryGetDto>> GetAllCountriesAsync()
    {
        var countries = await _countryRepository.GetAllAsync();
        return CountryMapper.MapToCountryGetDto(countries);
    }

    public async Task<CountryGetDto?> GetCountryByIdAsync(int id)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new EntityNotFoundException("Country", id);
        }
        return CountryMapper.MapToCountryGetDto(country);
    }

    public async Task<CountryGetDto> CreateCountryAsync(CountryCreateDto countryCreateDto)
    {
        await ValidateUniqueCountryName(countryCreateDto.Name);
        var country = CountryMapper.MapToCountry(countryCreateDto);
        await _countryRepository.CreateAsync(country);
        
        return CountryMapper.MapToCountryGetDto(country);
    }

    public async Task<CountryGetDto> UpdateCountryAsync(int id, CountryUpdateDto countryUpdateDto)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new EntityNotFoundException("Country", id);       
        }
        
        await ValidateUniqueCountryNameExcludingId(countryUpdateDto.Name, id);
        
        country.MapToCountry(countryUpdateDto);
        await _countryRepository.UpdateAsync(id, country);
        
        return CountryMapper.MapToCountryGetDto(country);
    }

    public async Task DeleteCountryAsync(int id)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new EntityNotFoundException("Country", id);
        }

        await _countryRepository.DeleteAsync(id);
    }

    private async Task ValidateUniqueCountryName(string name)
    {
        if (await _countryRepository.ExistsByNameAsync(name))
        {
            throw new DuplicateEntityException("Country", name);
        }
    }
    
    private async Task ValidateUniqueCountryNameExcludingId(string name, int id)
    {
        if (await _countryRepository.ExistsByNameExcludingIdAsync(name, id))
        {
            throw new DuplicateEntityException("Country", name);
        }
    }
}