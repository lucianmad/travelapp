using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface IAttractionRepository
{
    Task<IEnumerable<Attraction>> GetAllAsync(int? cityId = null, int? countryId = null, string? cityName = null, string? countryName = null);
    Task<Attraction?> GetByIdAsync(int id);
    Task<Attraction> CreateAsync(Attraction attraction);
    Task<Attraction> UpdateAsync(int id, Attraction attraction);
    Task DeleteAsync(int id);
    Task<bool> ExistsByNameAndCityAsync(string name, int cityId);
    Task<bool> ExistsByNameAndCityExcludingIdAsync(string name, int id, int cityId);
}