using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(string? countryName = null, int? countryId = null);
    Task<IEnumerable<City>> GetAllByCountryIdAsync(int countryId);
    Task<City?> GetByIdAsync(int id);
    Task<City> CreateAsync(City city);
    Task<City> UpdateAsync(int id, City city);
    Task DeleteAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameAndCountryAsync(string name, int countryId);
    Task<bool> ExistsByNameAndCountryExcludingIdAsync(string name, int id, int countryId);
}