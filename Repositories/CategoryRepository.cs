using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class CategoryRepository : ICategoryRepository
    {

        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdCategory, TitleCategory, LinkCategory, DescriptionCategory, LayersCategory FROM Categories";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var category = new Category
                            {
                                idCategory = reader.GetString(0),
                                titleCategory = reader.GetString(1),
                                linkCategory = reader.GetString(2),
                                descriptionCategory = reader.GetString(3),
                                layersCategory = reader.GetString(4)
                            };

                            categories.Add(category);
                        }
                }
            }
            return categories;
        }
        

        public async Task<Category> GetByIdAsync(string id)
        {
            Category category = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdCategory, TitleCategory, LinkCategory, DescriptionCategory, LayersCategory FROM Categories WHERE IdCategory = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            category = new Category
                            {
                                idCategory = reader.GetString(0),
                                titleCategory = reader.GetString(1),
                                linkCategory = reader.GetString(2),
                                descriptionCategory = reader.GetString(3),
                                layersCategory = reader.GetString(4)
                            };   
                            
                        }
                    }
                }
            }
            return category;
        }

        public async Task AddAsync(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Categories (IdCategory, TitleCategory, LinkCategory, DescriptionCategory, LayersCategory) VALUES (@IdCategory, @TitleCategory, @LinkCategory, @DescriptionCategory, @LayersCategory)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", category.idCategory);
                    command.Parameters.AddWithValue("@TitleCategory", category.titleCategory);
                    command.Parameters.AddWithValue("@LinkCategory", category.linkCategory);
                    command.Parameters.AddWithValue("@DescriptionCategory", category.descriptionCategory);
                    command.Parameters.AddWithValue("@LayersCategory", category.layersCategory);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Categories SET TitleCategory = @TitleCategory, LinkCategory = @LinkCategory, DescriptionCategory = @DescriptionCategory, LayersCategory = @LayersCategory WHERE IdCategory = @IdCategory";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", category.idCategory);
                    command.Parameters.AddWithValue("@TitleCategory", category.titleCategory);
                    command.Parameters.AddWithValue("@LinkCategory", category.linkCategory);
                    command.Parameters.AddWithValue("@DescriptionCategory", category.descriptionCategory);
                    command.Parameters.AddWithValue("@LayersCategory", category.layersCategory);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Categories WHERE IdCategory = @IdCategory";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategory", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }



        //métodos para tablas cruzadas
        public async Task<List<Category>> GetAllCategoriesByPropertyAsync(string propertyId)
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT CategoryId FROM PropertyCategories WHERE PropertyId = @PropertyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", propertyId);
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Category category = await GetByIdAsync(reader.GetString(0));
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }
        
        public async Task<List<Category>> GetAllCategoriesByEventAsync(string eventId)
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT CategoryId FROM EventCategories WHERE EventId = @EventId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Category category = await GetByIdAsync(reader.GetString(0));
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }
    }
}