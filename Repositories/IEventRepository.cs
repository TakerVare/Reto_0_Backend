using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{

    public interface IEventRepository
    {
        Task<List<Evento>> GetAllAsync();
        Task<Evento?> GetByIdAsync(string id);
        Task AddAsync(Evento evento);
        Task UpdateAsync(Evento evento);
        Task DeleteAsync(string id);
    }

}