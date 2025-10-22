using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories;
{

    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event evento);
        Task UpdateAsync(Event evento);
        Task DeleteAsync(string id);
        //Task InicializarDatosAsync();
    }



}