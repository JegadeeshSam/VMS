namespace VisitorManagementSystem.Repositories;

public interface IVisitRepository
{
    Task<IEnumerable<Visit>> GetAllAsync();
    Task<Visit?> GetByIdAsync(int id);
    Task AddAsync(Visit visit);
    Task UpdateAsync(Visit visit);
    Task DeleteAsync(int id);
    Task<IEnumerable<Visit>> GetTodayVisitsAsync();
    Task<IEnumerable<Visit>> GetVisitorsInsideAsync();
    Task<IEnumerable<Visit>> GetRecentVisitsAsync(int count);
}
