namespace VisitorManagementSystem.Repositories;

public interface IVisitorRepository
{
    Task<IEnumerable<Visitor>> GetAllAsync();
    Task<Visitor?> GetByIdAsync(int id);
    Task AddAsync(Visitor visitor);
    Task UpdateAsync(Visitor visitor);
    Task DeleteAsync(int id);
}
