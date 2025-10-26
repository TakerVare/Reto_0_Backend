using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class FeatureRepository : IFeatureRepository
    {

        private readonly string _connectionString;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IGeometryRepository _geometryRepository;

        public FeatureRepository(string connectionString,
                                IPropertyRepository propertyRepository,
                                IGeometryRepository geometryRepository)
        {
            _connectionString = connectionString;
            _propertyRepository = propertyRepository;
            _geometryRepository = geometryRepository;
        }

        public async Task<List<Feature>> GetAllAsync()
        {
            var features = new List<Feature>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FeatureId, GeometryId FROM Features";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var featureId = reader.GetString(0);
                            var geometryId = reader.IsDBNull(2) ? null : reader.GetString(2);
                            
                            var feature = new Feature
                            {
                                id = featureId,
                                properties = await _propertyRepository.GetAllPropertiesByFeatureAsync(featureId),
                                geometry = geometryId != null ? await _geometryRepository.GetByIdAsync(geometryId) : null
                            };

                            features.Add(feature);
                        }
                }
            }
            return features;
        }
        

        public async Task<Feature> GetByIdAsync(string id)
        {
            Feature feature = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FeatureId, GeometryId FROM Features WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var featureId = reader.GetString(0);
                            var geometryId = reader.IsDBNull(2) ? null : reader.GetString(2);
                            
                            feature = new Feature
                            {
                                id = featureId,
                                properties = await _propertyRepository.GetAllPropertiesByFeatureAsync(featureId),
                                geometry = geometryId != null ? await _geometryRepository.GetByIdAsync(geometryId) : null
                            };   
                        }
                    }
                }
            }
            return feature;
        }

        public async Task AddAsync(Feature feature)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Features (Id, FeatureId, GeometryId) VALUES (@Id, @FeatureId, @GeometryId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", feature.id);
                    command.Parameters.AddWithValue("@FeatureId", feature.id);
                    command.Parameters.AddWithValue("@GeometryId", (object)feature.geometry?.id ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
            //Pendiente de implementar métodos para insertar las listas de properties
        }

        public async Task UpdateAsync(Feature feature)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Features SET FeatureId = @FeatureId, GeometryId = @GeometryId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", feature.id);
                    command.Parameters.AddWithValue("@FeatureId", feature.id);
                    command.Parameters.AddWithValue("@GeometryId", (object)feature.geometry?.id ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Features WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}