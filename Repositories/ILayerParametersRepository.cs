using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface ILayerParametersRepository
    {
        Task<List<LayerParameters>> GetAllAsync();
        Task<LayerParameters?> GetByIdAsync(string id);
        Task AddAsync(LayerParameters layerParameters);
        Task UpdateAsync(LayerParameters layerParameters);
        Task DeleteAsync(string id);
        
        //Métodos para tablas cruzadas
        Task<List<LayerParameters>> GetAllParametersByLayerAsync(string layerId);
    }

}