using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.DataAccess.Repositories.Concretes;

public class CityRepository : ICityRepository
{
    private readonly TravelAppDbContext _context;

    public CityRepository(TravelAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetAllAsync(string? countryName = null, int? countryId = null)
    {
        var query = _context.Set<City>()
            .Include(c => c.Country)
            .Include(c => c.Attractions)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(countryName))
        {
            query = query.Where(c => c.Country.Name == countryName);
        }
        
        if (countryId.HasValue)
        {
            query = query.Where(c => c.CountryId == countryId);
        }
        
        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<City>> GetAllByCountryIdAsync(int countryId)
    {
        return await _context.Set<City>()
            .AsQueryable()
            .Where(c => c.CountryId == countryId)
            .ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _context.Set<City>()
            .Include(c => c.Country)
            .Include(c => c.Attractions)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<City> CreateAsync(City city)
    {
        await _context.Set<City>().AddAsync(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task<City> UpdateAsync(int id, City city)
    {
        var existingCity = await _context.Set<City>().FindAsync(id);
        if (existingCity == null)
        {
            return city;
        }
        _context.Entry(existingCity).CurrentValues.SetValues(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task DeleteAsync(int id)
    {
        var city = await _context.Set<City>().FindAsync(id);
        if (city == null)
        {
            return;
        }
        _context.Set<City>().Remove(city);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Set<City>().AnyAsync(c => c.Id == id);   
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<City>().AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
    
    public async Task<bool> ExistsByNameAndCountryAsync(string name, int countryId)
    {
        return await _context.Set<City>().AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.CountryId == countryId);;
    }

    public async Task<bool> ExistsByNameAndCountryExcludingIdAsync(string name, int id, int countryId)
    {
        return await _context.Set<City>().AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != id && c.CountryId == countryId);;
    }
}