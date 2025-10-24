using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories;
{

    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category plato);
        Task UpdateAsync(Category plato);
        Task DeleteAsync(string id);
        //Task InicializarDatosAsync();

         //Métodos para tablas cruzadas
        Task<List<Category>> GetAllCategoriesByPropertyAsync(string propertyId);

    }



}