using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.DataAccess.Repositories.Concretes;

public class CountryRepository : ICountryRepository
{
    private readonly TravelAppDbContext _context;

    public CountryRepository(TravelAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Set<Country>()
            .Include(c => c.Cities)
            .ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _context.Set<Country>()
            .Include(c => c.Cities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Country> CreateAsync(Country country)
    {
        await _context.Set<Country>().AddAsync(country);
        await _context.SaveChangesAsync();
        return country;
    }

    public async Task<Country> UpdateAsync(int id, Country country)
    {
        var existingCountry = await _context.Set<Country>().FindAsync(id);
        if (existingCountry == null)
        {
            return country;
        }
        _context.Entry(existingCountry).CurrentValues.SetValues(country);
        await _context.SaveChangesAsync();
        return country;
    }

    public async Task DeleteAsync(int id)
    {
        var country = await _context.Set<Country>().FindAsync(id);
        if (country == null)
        {
            return;
        }
        _context.Set<Country>().Remove(country);
        await _context.SaveChangesAsync();
    }
}