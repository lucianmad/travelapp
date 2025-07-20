using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface IAttractionRepository
{
    Task<IEnumerable<Attraction>> GetAllAsync();
    Task<Attraction?> GetByIdAsync(int id);
    Task<Attraction> CreateAsync(Attraction attraction);
    Task<Attraction> UpdateAsync(int id, Attraction attraction);
    Task DeleteAsync(int id);
}