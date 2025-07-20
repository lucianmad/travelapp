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
    
    public async Task<IEnumerable<Attraction>> GetAllAsync()
    {
        return await _context.Set<Attraction>()
            .Include(a => a.City)
                .ThenInclude(c => c.Country)
            .ToListAsync();
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
}