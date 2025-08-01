using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<Country> CreateAsync(Country country);
    Task<Country> UpdateAsync(int id, Country country);
    Task DeleteAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameExcludingIdAsync(string name, int id);
}