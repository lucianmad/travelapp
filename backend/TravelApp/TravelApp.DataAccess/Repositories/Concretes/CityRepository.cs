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

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Set<City>()
            .Include(c => c.Country)
            .Include(c => c.Attractions)
            .ToListAsync();
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
}