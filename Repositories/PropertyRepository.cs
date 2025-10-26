using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class PropertyRepository : IPropertyRepository
    {

        private readonly string _connectionString;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISourceRepository _sourceRepository;

        public PropertyRepository(string connectionString,
                                 ICategoryRepository categoryRepository,
                                 ISourceRepository sourceRepository)
        {
            _connectionString = connectionString;
            _categoryRepository = categoryRepository;
            _sourceRepository = sourceRepository;
        }

        public async Task<List<Property>> GetAllAsync()
        {
            var properties = new List<Property>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PropertyId, Title, Description, Link, Closed, Date, MagnitudeValue, MagnitudeUnit FROM Properties";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var propertyId = reader.GetString(0);
                            var property = new Property
                            {
                                id = propertyId,
                                title = reader.GetString(1),
                                description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                link = reader.GetString(3),
                                closed = reader.IsDBNull(4) ? null : reader.GetString(4),
                                date = reader.GetDateTime(5),
                                magnitudeValue = reader.GetDouble(6),
                                magnitudeUnit = reader.IsDBNull(7) ? null : reader.GetString(7), 
                                categories = await _categoryRepository.GetAllCategoriesByPropertyAsync(propertyId),
                                sources = await _sourceRepository.GetAllSourcesByPropertyAsync(propertyId)
                            };

                            properties.Add(property);
                        }
                }
            }
            return properties;
        }
        

        public async Task<Property> GetByIdAsync(string id)
        {
            Property property = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PropertyId, Title, Description, Link, Closed, Date, MagnitudeValue, MagnitudeUnit FROM Properties WHERE PropertyId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var propertyId = reader.GetString(0);
                            property = new Property
                            {
                                id = propertyId,
                                title = reader.GetString(1),
                                description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                link = reader.GetString(3),
                                closed = reader.IsDBNull(4) ? null : reader.GetString(4),
                                date = reader.GetDateTime(5),
                                magnitudeValue = reader.GetDouble(6),
                                magnitudeUnit = reader.IsDBNull(7) ? null : reader.GetString(7), 
                                categories = await _categoryRepository.GetAllCategoriesByPropertyAsync(propertyId),
                                sources = await _sourceRepository.GetAllSourcesByPropertyAsync(propertyId)
                            };   
                        }
                    }
                }
            }
            return property;
        }

        public async Task AddAsync(Property property)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Properties (PropertyId, Title, Description, Link, Closed, Date, MagnitudeValue, MagnitudeUnit) VALUES (@PropertyId, @Title, @Description, @Link, @Closed, @Date, @MagnitudeValue, @MagnitudeUnit)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", property.id);
                    command.Parameters.AddWithValue("@Title", property.title);
                    command.Parameters.AddWithValue("@Description", (object)property.description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Link", property.link);
                    command.Parameters.AddWithValue("@Closed", (object)property.closed ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Date", property.date);
                    command.Parameters.AddWithValue("@MagnitudeValue", property.magnitudeValue);
                    command.Parameters.AddWithValue("@MagnitudeUnit", (object)property.magnitudeUnit ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
            //Pendiente de implementar métodos para insertar las listas de categorías y fuentes
        }

        public async Task UpdateAsync(Property property)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Properties SET Title = @Title, Description = @Description, Link = @Link, Closed = @Closed, Date = @Date, MagnitudeValue = @MagnitudeValue, MagnitudeUnit = @MagnitudeUnit WHERE PropertyId = @PropertyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", property.id);
                    command.Parameters.AddWithValue("@Title", property.title);
                    command.Parameters.AddWithValue("@Description", (object)property.description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Link", property.link);
                    command.Parameters.AddWithValue("@Closed", (object)property.closed ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Date", property.date);
                    command.Parameters.AddWithValue("@MagnitudeValue", property.magnitudeValue);
                    command.Parameters.AddWithValue("@MagnitudeUnit", (object)property.magnitudeUnit ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Properties WHERE PropertyId = @PropertyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PropertyId", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        // MÉTODOS PARA TABLAS CRUZADAS

        // FeatureProperties
        public async Task<List<Property>> GetAllPropertiesByFeatureAsync(string featureId)
        {
            var properties = new List<Property>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PropertyId FROM FeatureProperties WHERE FeatureId = @FeatureId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FeatureId", featureId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Property property = await GetByIdAsync(reader.GetString(0));
                            properties.Add(property);
                        }
                    }
                }
            }
            return properties;
        }
    }
}