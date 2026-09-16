using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly ApplicationDbContext _context;

    public VisitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Visit>> GetAllAsync()
    {
        return await _context.Visits
            .Include(v => v.Visitor)
            .Include(v => v.Employee)
            .OrderByDescending(v => v.CheckInTime)
            .ToListAsync();
    }

    public async Task<Visit?> GetByIdAsync(int id)
    {
        return await _context.Visits
            .Include(v => v.Visitor)
            .Include(v => v.Employee)
            .FirstOrDefaultAsync(v => v.VisitId == id);
    }

    public async Task AddAsync(Visit visit)
    {
        await _context.Visits.AddAsync(visit);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Visit visit)
    {
        _context.Visits.Update(visit);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var visit = await _context.Visits.FindAsync(id);
        if (visit != null)
        {
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Visit>> GetTodayVisitsAsync()
    {
        var today = DateTime.Today;
        return await _context.Visits
            .Include(v => v.Visitor)
            .Include(v => v.Employee)
            .Where(v => v.VisitDate.Date == today)
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetVisitorsInsideAsync()
    {
        return await _context.Visits
            .Include(v => v.Visitor)
            .Include(v => v.Employee)
            .Where(v => v.Status == "CheckedIn")
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetRecentVisitsAsync(int count)
    {
        return await _context.Visits
            .Include(v => v.Visitor)
            .Include(v => v.Employee)
            .OrderByDescending(v => v.CheckInTime)
            .Take(count)
            .ToListAsync();
    }
}
