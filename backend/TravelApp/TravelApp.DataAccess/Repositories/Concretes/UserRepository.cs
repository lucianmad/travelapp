using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.DataAccess.Repositories.Concretes;

public class UserRepository : IUserRepository
{
    private readonly TravelAppDbContext _context;

    public UserRepository(TravelAppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(int id, User user)
    {
        var existingUser = await _context.Set<User>().FindAsync(id);
        if (existingUser == null)
        {
            return user;
        }
        _context.Entry(existingUser).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Set<User>().FindAsync(id);
        if (user == null)
        {
            return;
        }
        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email, string username)
    {
        return await _context.Set<User>().AnyAsync(u => u.Email == email || u.Username == username);
    }
}