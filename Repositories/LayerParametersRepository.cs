using Microsoft.Data.SqlClient;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Repositories
{
    
    public class LayerParametersRepository : ILayerParametersRepository
    {

        private readonly string _connectionString;

        public LayerParametersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<LayerParameters>> GetAllAsync()
        {
            var layerParametersList = new List<LayerParameters>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, TILEMATRIXSET, FORMAT FROM LayerParameters";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {
                            var layerParameters = new LayerParameters
                            {
                                id = reader.GetString(0),
                                TILEMATRIXSET = reader.IsDBNull(1) ? null : reader.GetString(1),
                                FORMAT = reader.IsDBNull(2) ? null : reader.GetString(2)
                            };

                            layerParametersList.Add(layerParameters);
                        }
                }
            }
            return layerParametersList;
        }
        

        public async Task<LayerParameters> GetByIdAsync(string id)
        {
            LayerParameters layerParameters = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, TILEMATRIXSET, FORMAT FROM LayerParameters WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            layerParameters = new LayerParameters
                            {
                                id = reader.GetString(0),
                                TILEMATRIXSET = reader.IsDBNull(1) ? null : reader.GetString(1),
                                FORMAT = reader.IsDBNull(2) ? null : reader.GetString(2)
                            };   
                        }
                    }
                }
            }
            return layerParameters;
        }

        public async Task AddAsync(LayerParameters layerParameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO LayerParameters (Id, TILEMATRIXSET, FORMAT) VALUES (@Id, @TILEMATRIXSET, @FORMAT)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", layerParameters.id);
                    command.Parameters.AddWithValue("@TILEMATRIXSET", (object)layerParameters.TILEMATRIXSET ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FORMAT", (object)layerParameters.FORMAT ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(LayerParameters layerParameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE LayerParameters SET TILEMATRIXSET = @TILEMATRIXSET, FORMAT = @FORMAT WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", layerParameters.id);
                    command.Parameters.AddWithValue("@TILEMATRIXSET", (object)layerParameters.TILEMATRIXSET ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FORMAT", (object)layerParameters.FORMAT ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        } 
        
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM LayerParameters WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        // MÉTODOS PARA TABLAS CRUZADAS

        // LayerParametersRelation
        public async Task<List<LayerParameters>> GetAllParametersByLayerAsync(string layerId)
        {
            var layerParametersList = new List<LayerParameters>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT ParameterId FROM LayerParametersRelation WHERE LayerId = @LayerId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LayerId", layerId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            LayerParameters layerParameters = await GetByIdAsync(reader.GetString(0));
                            layerParametersList.Add(layerParameters);
                        }
                    }
                }
            }
            return layerParametersList;
        }
    }
}