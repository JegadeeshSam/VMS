using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Repositories;

public class VisitorRepository : IVisitorRepository
{
    private readonly ApplicationDbContext _context;

    public VisitorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Visitor>> GetAllAsync()
    {
        return await _context.Visitors.ToListAsync();
    }

    public async Task<Visitor?> GetByIdAsync(int id)
    {
        return await _context.Visitors.FindAsync(id);
    }

    public async Task AddAsync(Visitor visitor)
    {
        await _context.Visitors.AddAsync(visitor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Visitor visitor)
    {
        _context.Visitors.Update(visitor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var visitor = await _context.Visitors.FindAsync(id);
        if (visitor != null)
        {
            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
        }
    }
}
