using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface ILayerRepository
    {
        Task<List<Layer>> GetAllAsync();
        Task<Layer?> GetByIdAsync(string id);
        Task AddAsync(Layer layer);
        Task UpdateAsync(Layer layer);
        Task DeleteAsync(string id);
    }

}