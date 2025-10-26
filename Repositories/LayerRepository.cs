using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class LayerRepository : ILayerRepository
    {

        private readonly string _connectionString;
        private readonly ILayerParametersRepository _layerParametersRepository;

        public LayerRepository(string connectionString,
                              ILayerParametersRepository layerParametersRepository)
        {
            _connectionString = connectionString;
            _layerParametersRepository = layerParametersRepository;
        }

        public async Task<List<Layer>> GetAllAsync()
        {
            var layers = new List<Layer>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ServiceUrl, ServiceTypeId FROM Layers";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var layerId = reader.GetString(0);
                            var layer = new Layer
                            {
                                id = layerId,
                                name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                serviceUrl = reader.IsDBNull(2) ? null : reader.GetString(2),
                                serviceTypeId = reader.IsDBNull(3) ? null : reader.GetString(3),
                                parameters = await _layerParametersRepository.GetAllParametersByLayerAsync(layerId)
                            };

                            layers.Add(layer);
                        }
                }
            }
            return layers;
        }
        

        public async Task<Layer> GetByIdAsync(string id)
        {
            Layer layer = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ServiceUrl, ServiceTypeId FROM Layers WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var layerId = reader.GetString(0);
                            layer = new Layer
                            {
                                id = layerId,
                                name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                serviceUrl = reader.IsDBNull(2) ? null : reader.GetString(2),
                                serviceTypeId = reader.IsDBNull(3) ? null : reader.GetString(3),
                                parameters = await _layerParametersRepository.GetAllParametersByLayerAsync(layerId)
                            };   
                        }
                    }
                }
            }
            return layer;
        }

        public async Task AddAsync(Layer layer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Layers (Id, Name, ServiceUrl, ServiceTypeId) VALUES (@Id, @Name, @ServiceUrl, @ServiceTypeId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", layer.id);
                    command.Parameters.AddWithValue("@Name", (object)layer.name ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServiceUrl", (object)layer.serviceUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServiceTypeId", (object)layer.serviceTypeId ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
            //Pendiente de implementar métodos para insertar la lista de parameters
        }

        public async Task UpdateAsync(Layer layer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Layers SET Name = @Name, ServiceUrl = @ServiceUrl, ServiceTypeId = @ServiceTypeId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", layer.id);
                    command.Parameters.AddWithValue("@Name", (object)layer.name ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServiceUrl", (object)layer.serviceUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServiceTypeId", (object)layer.serviceTypeId ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Layers WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}