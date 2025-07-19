using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.DataAccess.Repositories.Concretes;

public class GenericRepository<T> : IGenericRepository<T>  where T : class
{
    private readonly TravelAppDbContext _context;

    public GenericRepository(TravelAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(int id, T entity)
    {
        var entityToUpdate = await _context.Set<T>().FindAsync(id);
        if (entityToUpdate == null)
        {
            return entity;
        }
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return;
        }
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}