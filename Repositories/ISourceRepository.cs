using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories;
{

    public interface ISourceRepository
    {
        Task<List<Source>> GetAllAsync();
        Task<Source?> GetByIdAsync(int id);
        Task AddAsync(Source geometry);
        Task UpdateAsync(Source geometry);
        Task DeleteAsync(string id);
        //Task InicializarDatosAsync();
        
        //Métodos para tablas cruzadas
        Task<List<Source>> GetAllByPropertyAsync(string propertyId);

        Task<List<Source>> GetAllSourcesByPropertyAsync(string propertyId);
        

    }



}