using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface ISourceRepository
    {
        Task<List<Source>> GetAllAsync();
        Task<Source?> GetByIdAsync(string id);
        Task AddAsync(Source source);
        Task UpdateAsync(Source source);
        Task DeleteAsync(string id);

        //Métodos para tablas cruzadas
        Task<List<Source>> GetAllSourcesByPropertyAsync(string propertyId);
        Task<List<Source>> GetAllSourcesByEventAsync(string eventId);
    }

}