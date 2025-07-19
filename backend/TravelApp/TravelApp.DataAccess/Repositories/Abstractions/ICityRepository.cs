using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<IEnumerable<City>> GetAllByCountryIdAsync(int countryId);
    Task<City?> GetByIdAsync(int id);
    Task<City> CreateAsync(City city);
    Task<City> UpdateAsync(int id, City city);
    Task DeleteAsync(int id);
}