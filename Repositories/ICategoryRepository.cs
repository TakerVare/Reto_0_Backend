using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);

        //Métodos para tablas cruzadas
        Task<List<Category>> GetAllCategoriesByPropertyAsync(string propertyId);
        Task<List<Category>> GetAllCategoriesByEventAsync(string eventId);
    }

}