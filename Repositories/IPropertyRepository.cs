using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories;
{

    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(int id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(string id);
        //Task InicializarDatosAsync();
        
        //Métodos para tablascruzadas
        //Task<List<Property>> GetAllByPropertyAsync(string propertyId);

    }



}