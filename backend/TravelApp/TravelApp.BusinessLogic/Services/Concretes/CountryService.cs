using TravelApp.BusinessLogic.DTOs.Country;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class CountryService : ICountryService
{
    private IGenericRepository<Country> _countryRepository;

    public CountryService(IGenericRepository<Country> countryRepository)
    {
        _countryRepository = countryRepository;
    }
    
    public async Task<IEnumerable<CountryGetDto>> GetAllCountriesAsync()
    {
        var countries = await _countryRepository.GetAllAsync();
        return countries.Select(c => new CountryGetDto
        {
            Id = c.Id, 
            Name = c.Name, 
            Cities = c.Cities, 
            Flag = c.Flag
        });
    }

    public async Task<CountryGetDto> GetCountryByIdAsync(int id)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new ArgumentException($"Country with id {id} was not found");
        }

        return new CountryGetDto
        {
            Id = country.Id,
            Name = country.Name,
            Cities = country.Cities,
            Flag = country.Flag
        };
    }

    public async Task<CountryGetDto> CreateCountryAsync(CountryCreateDto countryCreateDto)
    {
        var country = new Country
        {
            Name = countryCreateDto.Name,
            Flag = countryCreateDto.Flag
        };
        await _countryRepository.CreateAsync(country);
        
        return new CountryGetDto
        {
            Id = country.Id,
            Name = country.Name,
            Flag = country.Flag
        };
    }
}