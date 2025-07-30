using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.DataAccess.Repositories.Concretes;

public class AttractionRepository : IAttractionRepository
{
    private readonly TravelAppDbContext _context;

    public AttractionRepository(TravelAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Attraction>> GetAllAsync(int? cityId = null, int? countryId = null, string? cityName = null, string? countryName = null)
    {
        var query = _context.Set<Attraction>()
            .Include(a => a.City)
                .ThenInclude(c => c.Country)
            .AsQueryable();
        
        if (cityId.HasValue)
        {
            query = query.Where(a => a.CityId == cityId);
        }
        
        if (countryId.HasValue)
        {
            query = query.Where(a => a.City.CountryId == countryId);
        }

        if (!string.IsNullOrEmpty(cityName))
        {
            query = query.Where(a => a.City.Name == cityName);
        }
        
        if (!string.IsNullOrEmpty(countryName))
        {
            query = query.Where(a => a.City.Country.Name == countryName);
        }
        
        return await query.ToListAsync();
    }

    public async Task<Attraction?> GetByIdAsync(int id)
    {
        return await  _context.Set<Attraction>()
            .Include(a => a.City)
                .ThenInclude(c => c.Country)
            .Include(a => a.Reviews)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Attraction> CreateAsync(Attraction attraction)
    {
        await _context.Set<Attraction>().AddAsync(attraction);
        await _context.SaveChangesAsync();
        return attraction;
    }

    public async Task<Attraction> UpdateAsync(int id, Attraction attraction)
    {
        var existingAttraction = await _context.Set<Attraction>().FindAsync(id);
        if (existingAttraction == null)
        {
            return attraction;
        }
        _context.Entry(existingAttraction).CurrentValues.SetValues(attraction);
        await _context.SaveChangesAsync();
        return attraction;
    }

    public async Task DeleteAsync(int id)
    {
        var attraction = await _context.Set<Attraction>().FindAsync(id);
        if (attraction == null)
        {
            return;
        }
        _context.Set<Attraction>().Remove(attraction);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<Attraction>().AnyAsync(a => a.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> ExistsByNameExcludingIdAsync(string name, int id)
    {
        return await _context.Set<Attraction>().AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.Id != id);
    }

    public async Task<bool> ExistsByNameAndCityAsync(string name, int cityId)
    {
        return await _context.Set<Attraction>().AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.CityId == cityId);
    }
    
    public async Task<bool> ExistsByNameAndCityExcludingIdAsync(string name, int id, int cityId)
    {
        return await _context.Set<Attraction>().AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.Id != id && a.CityId == cityId);
    }
}