using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface IGeometryRepository
    {
        Task<List<Geometry>> GetAllAsync();
        Task<Geometry?> GetByIdAsync(string id);
        Task AddAsync(Geometry geometry);
        Task UpdateAsync(Geometry geometry);
        Task DeleteAsync(string id);
        
        //Métodos para tablas cruzadas
        Task<List<Geometry>> GetAllGeometryByEventAsync(string eventId);
    }

}