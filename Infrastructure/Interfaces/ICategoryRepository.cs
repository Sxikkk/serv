using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
}