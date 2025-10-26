using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAllAsync();
        Task<Feature?> GetByIdAsync(string id);
        Task AddAsync(Feature feature);
        Task UpdateAsync(Feature feature);
        Task DeleteAsync(string id);
    }

}