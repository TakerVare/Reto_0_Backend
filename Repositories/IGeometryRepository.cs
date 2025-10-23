using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories;
{

    public interface ICategoryRepository
    {
        Task<List<Geometry>> GetAllAsync();
        Task<Geometry?> GetByIdAsync(int id);
        Task AddAsync(Geometry geometry);
        Task UpdateAsync(Geometry geometry);
        Task DeleteAsync(string id);
        //Task InicializarDatosAsync();
    }



}