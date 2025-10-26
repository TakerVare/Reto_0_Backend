using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(string id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(string id);
        
        //Métodos para tablas cruzadas
        Task<List<Property>> GetAllPropertiesByFeatureAsync(string featureId);
    }

}